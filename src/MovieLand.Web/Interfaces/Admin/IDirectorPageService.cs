using MovieLand.Web.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace MovieLand.Web.Interfaces.Admin
{
    public interface IDirectorPageService
    {
        Task AddDirector(DirectorViewModel director);
        Task DeleteDirector(int directorId);
        Task UpdateDirector(DirectorViewModel director);

        Task<DirectorViewModel> GetDirectorById(int directorId);

        Task<IEnumerable<DirectorViewModel>> GetDirectors();
    }
}
