using DTO.ViewModels;
using Microsoft.AspNetCore.Http;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IMovieService
    {
        Task<GetAllMoviesVM> GetAllMovies();
        Task<MovieVM> GetMovieById(Guid id);
        Task AddMovie(MovieCreateVM movie);
        Task UpdateMovie(Guid Id, MovieUpdateVM movie);
        Task DeleteMovie(Guid id);
        Task<decimal> GetAverageRating(Guid id);
        Task UploadFile(IFormFile file, string path);
        Task UpdateImage(UpdateImageVM movie);
    }
}
