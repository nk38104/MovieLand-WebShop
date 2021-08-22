using MovieLand.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieLand.Web.Interfaces
{
    public interface IMoviePageService
    {
        Task<IEnumerable<MovieViewModel>> GetProducts(string productName);
        Task<MovieViewModel> GetProductById(int productId);
        Task<MovieViewModel> GetProductBySlug(string slug);
    }
}
