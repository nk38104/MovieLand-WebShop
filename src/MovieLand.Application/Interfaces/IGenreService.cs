using MovieLand.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace MovieLand.Application.Interfaces
{
    public interface IGenreService
    {
        Task AddGenre(GenreDTO genre);
        Task DeleteGenre(int genreId);
        Task UpdateGenre(GenreDTO genre);

        Task<GenreDTO> GetGenreById(int genreId);

        Task<IEnumerable<GenreDTO>> GetGenreList();
    }
}
