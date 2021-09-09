using MovieLand.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace MovieLand.Application.Interfaces
{
    public interface IMovieService
    {
        Task AddMovie(CreateMovieDTO newMovie, List<int> genreIds, List<int> directorIds);
        Task DeleteMovie(int movieId);

        Task<MovieDTO> GetMovieById(int movieId);
        Task<MovieDTO> GetMovieBySlug(string slug);

        Task<IEnumerable<MovieDTO>> GetMovieByTitle(string movieTitle);
        
        Task<IEnumerable<MovieDTO>> GetMovieList();
        Task<IEnumerable<MovieDTO>> GetMoviesByDecade(string decade);
        Task<IEnumerable<MovieDTO>> GetMoviesByDirector(string director);
        Task<IEnumerable<MovieDTO>> GetMoviesByGenre(string genre);
        Task<IEnumerable<MovieDTO>> GetMoviesByPrice(double priceFrom, double priceTo);
        Task<IEnumerable<MovieDTO>> GetMoviesByRating(double rating);
    }
}
