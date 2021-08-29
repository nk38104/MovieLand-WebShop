using MovieLand.Web.ViewModels;
using System.Threading.Tasks;


namespace MovieLand.Web.Interfaces
{
    public interface IComparePageService
    {
        Task RemoveItem(int compareId, int movieId);

        Task<CompareViewModel> GetCompare(string username);
    }
}
