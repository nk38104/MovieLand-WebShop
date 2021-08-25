using MovieLand.Application.DTOs;
using System.Threading.Tasks;


namespace MovieLand.Application.Interfaces
{
    public interface ICompareService
    {
        Task<CompareDTO> GetCompareByUsername(string username);
        Task AddItem(string username, int movieId);
        Task RemoveItem(int compareId, int movieId);
    }
}
