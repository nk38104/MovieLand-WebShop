﻿using MovieLand.Web.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace MovieLand.Web.Interfaces
{
    public interface IMoviePageService
    {
        Task<IEnumerable<MovieViewModel>> GetMovies(string movieTitle);
        Task<MovieViewModel> GetMovieById(int movieId);
        Task<MovieViewModel> GetMovieBySlug(string slug);

        Task AddToFavorites(string username, int movieId);
        Task AddToCompare(string username, int movieId);
    }
}
