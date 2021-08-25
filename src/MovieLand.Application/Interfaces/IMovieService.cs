using MovieLand.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace MovieLand.Application.Interfaces
{
    public interface IMovieService
    {
        Task<IEnumerable<MovieDTO>> GetMovieList();
        Task<IEnumerable<MovieDTO>> GetMovieByTitle(string productName);
        Task<MovieDTO> GetMovieById(int productId);
        Task<MovieDTO> GetMovieBySlug(string slug);
    }
}
