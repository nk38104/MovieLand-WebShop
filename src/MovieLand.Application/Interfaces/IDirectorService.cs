using MovieLand.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace MovieLand.Application.Interfaces
{
    public interface IDirectorService
    {
        Task<IEnumerable<DirectorDTO>> GetDirectorList();
    }
}
