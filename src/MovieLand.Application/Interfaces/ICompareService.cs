using MovieLand.Application.DTOs;
using System.Threading.Tasks;


namespace MovieLand.Application.Interfaces
{
    public interface ICompareService
    {
        Task AddItem(string username, int movieId);
        Task RemoveItem(int compareId, int movieId);
        
        Task<CompareDTO> GetCompareByUsername(string username);
    }
}
