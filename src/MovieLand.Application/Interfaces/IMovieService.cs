using MovieLand.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace MovieLand.Application.Interfaces
{
    public interface IMovieService
    {
        Task<IEnumerable<MovieDTO>> GetMovieList();
        Task<IEnumerable<MovieDTO>> GetMovieByTitle(string movieTitle);
        Task<MovieDTO> GetMovieById(int movieId);
        Task<MovieDTO> GetMovieBySlug(string slug);
    }
}
