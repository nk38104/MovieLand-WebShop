using MovieLand.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace MovieLand.Application.Interfaces
{
    public interface IGenreService
    {
        Task<IEnumerable<GenreDTO>> GetGenreList();
    }
}
