using Azure;
using DAL.Repository.Interfaces;
using DTO.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;
using Services.Interfaces;
using System.Data;
using System.Net;

namespace MovieAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MovieController : ControllerBase
    {
        private readonly IMovieService _movieService;
        public MovieController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpGet]
        [Route("~/api/GetAllMovies")]
        public async Task<Respo<GetAllMoviesVM>> GetAllMovies()
        {
            try
            {
                var movies = await _movieService.GetAllMovies();
                if (movies.Movies.Count() >= 1)
                {
                    return new Respo<GetAllMoviesVM> { Status = "Success", HttpStatus = HttpStatusCode.OK, Message = "Data Found", Data = movies };
                }
                return new Respo<GetAllMoviesVM> { Status = "Error", HttpStatus = HttpStatusCode.NotFound, Message = "No Movies were found" };
            }
            catch (Exception ex)
            {
                return new Respo<GetAllMoviesVM> { Status = "Error", Message = ex.Message, HttpStatus = HttpStatusCode.InternalServerError };
            }

        }
        [HttpPost]
        [Route("~/api/AddMovie")]
        public async Task<Respo<MovieCreateVM>> AddMovie([FromForm]MovieCreateVM movie)
        {
            try
            {
                await _movieService.AddMovie(movie);
                return new Respo<MovieCreateVM> { Status = "Success", Message = "Movie Added Successfully.", HttpStatus = HttpStatusCode.OK, Data = movie };
            }
            catch (Exception ex)
            {
                return new Respo<MovieCreateVM> { Status = "Error", Message = ex.Message, HttpStatus = HttpStatusCode.InternalServerError };
            }
        }
        [HttpGet]
        [Route("~/api/GetMovieById/{MovieId}")]
        public async Task<Respo<MovieVM>> GetMovieById(Guid MovieId)
        {
            try
            {
                var movie = await _movieService.GetMovieById(MovieId);
                if (movie != null)
                {
                    return new Respo<MovieVM> { Status = "Success", Message = "Movie Found", HttpStatus = HttpStatusCode.OK, Data = movie };
                }
                return new Respo<MovieVM> { Status = "Error", Message = "No Movie of given ID can be found", HttpStatus = HttpStatusCode.NotFound };
            }
            catch (Exception ex)
            {
                return new Respo<MovieVM> { Status = "Error", Message = ex.Message, HttpStatus = HttpStatusCode.InternalServerError };
            }
        }
        [HttpPut]
        [Route("~/api/UpdateMovie")]
        public async Task<Respo<MovieUpdateVM>> UpdateMovie(Guid MovieId, MovieUpdateVM movie)
        {
            try
            {
                await _movieService.UpdateMovie(MovieId, movie);
                return new Respo<MovieUpdateVM> { Status = "Success", Message = "Movie Updated Successfully", HttpStatus = HttpStatusCode.OK, Data = movie };
            }
            catch (Exception ex)
            {
                return new Respo<MovieUpdateVM> { Status = "Error", Message = ex.Message, HttpStatus = HttpStatusCode.InternalServerError };
            }
        }
        [HttpDelete]
        [Route("~/api/DeleteMovie/{MovieId}")]
        public async Task<Respo<string>> DeleteMovie(Guid MovieId)
        {
            try
            {
                if (await _movieService.GetMovieById(MovieId) != null)
                {
                    await _movieService.DeleteMovie(MovieId);
                    return new Respo<string> { Status = "Success", Message = "Movie Deleted Successfully", HttpStatus = HttpStatusCode.NoContent };
                }
                return new Respo<string> { Status = "Not Found", Message = "Movie Not Found", HttpStatus = HttpStatusCode.NotFound };
            }
            catch (Exception ex)
            {
                return new Respo<string> { Status = "Error", Message = ex.Message, HttpStatus = HttpStatusCode.InternalServerError };
            }
        }

        [HttpPatch]
        [Route("~/api/UpdateImage")]
        public async Task<Respo<string>> UpdateImage([FromForm] UpdateImageVM update)
        {
            try
            {
                await _movieService.UpdateImage(update);
                return new Respo<string> { Status = "Success", HttpStatus = HttpStatusCode.OK, Message = "Image Changed Successfully" };
            }
            catch (Exception ex)
            {
                return new Respo<string> { Status = "Error", Message = ex.Message, HttpStatus = HttpStatusCode.InternalServerError };
            }
        }

    }
}
