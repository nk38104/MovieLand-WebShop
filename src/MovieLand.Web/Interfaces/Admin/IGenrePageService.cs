using MovieLand.Web.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace MovieLand.Web.Interfaces.Admin
{
    public interface IGenrePageService
    {
        Task AddGenre(GenreViewModel genre);
        Task DeleteGenre(int genreId);
        Task UpdateGenre(GenreViewModel genre);

        Task<GenreViewModel> GetGenreById(int genreId);

        Task<IEnumerable<GenreViewModel>> GetGenres();
    }
}
