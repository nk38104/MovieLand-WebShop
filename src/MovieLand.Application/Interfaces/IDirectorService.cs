using MovieLand.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace MovieLand.Application.Interfaces
{
    public interface IDirectorService
    {
        Task AddDirector(DirectorDTO director);
        Task DeleteDirector(int directorId);
        Task UpdateDirector(DirectorDTO director);

        Task<DirectorDTO> GetDirectorById(int directorId);

        Task<IEnumerable<DirectorDTO>> GetDirectorList();
    }
}
