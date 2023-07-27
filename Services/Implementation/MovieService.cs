using Azure;
using DAL.Repository.Interfaces;
using DTO.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Model;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Services.Implementation
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly IHostingEnvironment _environment;

        public MovieService(IMovieRepository movieRepository, IHostingEnvironment environment, UserManager<AppUser> userManager)
        {
            _movieRepository = movieRepository;
            _environment = environment;
            _userManager = userManager;
        }

        public async Task AddMovie(MovieCreateVM movie)
        {
            try
            {
                var path = "C:\\Users\\prash\\OneDrive\\Desktop\\Projects\\MovieAPI\\MovieAPI\\";
                var filePath = "Images/" + movie.ImagePath.FileName;
                var fullPath = Path.Combine(path, filePath);
                UploadFile(movie.ImagePath, fullPath);
                var newmovie = new Movie()
                {
                    MovieId = Guid.NewGuid(),
                    MovieName = movie.MovieName,
                    MovieDescription = movie.MovieDescription,
                    MovieGenre = movie.MovieGenre,
                    ImagePath = filePath
                };
                await _movieRepository.AddMovie(newmovie);
            }
            catch (Exception ex)
            {
                var err = ex.ToString();
            }
        }

        public async Task DeleteMovie(Guid id)
        {
            await _movieRepository.DeleteMovie(id);   
           // throw new NotImplementedException();
        }

        public async Task<GetAllMoviesVM> GetAllMovies()
        {
            var movies = await _movieRepository.GetAllMovies();

            IEnumerable<MovieVM> movieVMs = movies.Select(movie => new MovieVM
            {
                MovieId = movie.MovieId,
                MovieName = movie.MovieName,
                MovieDescription = movie.MovieDescription,
                MovieGenre = movie.MovieGenre,
                ImagePath = movie.ImagePath,
                AverageRating = movie.AverageRating
            });

            GetAllMoviesVM getAllMoviesVM = new GetAllMoviesVM()
            {
                Movies = movieVMs
            };
            return getAllMoviesVM;
        }

        public Task<decimal> GetAverageRating(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<MovieVM> GetMovieById(Guid id)
        {
            var movie = _movieRepository.GetMovieById(id);
            MovieVM vm = new MovieVM()
            {
                MovieId = movie.Result.MovieId,
                MovieName =movie.Result.MovieName,
                MovieDescription = movie.Result.MovieDescription,
                MovieGenre = movie.Result.MovieGenre,
                AverageRating=movie.Result.AverageRating,
                ImagePath=movie.Result?.ImagePath
            };
            return vm;
            //throw new NotImplementedException();
        }

        public async Task UpdateMovie(Guid Id, MovieUpdateVM movie)
        {
            var existingMovie = await _movieRepository.GetMovieById(Id);

            existingMovie.MovieName = movie.MovieName;
            existingMovie.MovieDescription = movie.MovieDescription;
            existingMovie.MovieGenre = movie.MovieGenre;
            await _movieRepository.UpdateMovie(existingMovie);
         
            // throw new NotImplementedException();
        }
        public async Task UpdateImage(UpdateImageVM movie)
        {
            try
            {
                var data = await _movieRepository.GetMovieById(movie.MovieId);
                var path = "C:\\Users\\prash\\OneDrive\\Desktop\\Projects\\MovieAPI\\MovieAPI\\";
                var filePath = "Images/" + movie.ImagePath.FileName;
                var fullPath = Path.Combine(path, filePath);
                UploadFile(movie.ImagePath, fullPath);

                data.ImagePath = filePath;

                //  await _dbContext.SaveChangesAsync();
                await _movieRepository.UpdateMovie(data);
            }
            catch (DbUpdateException ex)
            {
                var message = ex.Message;
            }
        }

        public async Task UploadFile(IFormFile file, string path)
        {
            FileStream stream = new FileStream(path, FileMode.Create);
            file.CopyTo(stream);
        }
    }
}
