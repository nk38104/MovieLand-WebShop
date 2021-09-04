using MovieLand.Domain.Entities;
using MovieLand.Domain.Interfaces.Repositories.Base;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace MovieLand.Domain.Interfaces.Repositories
{
    public interface IReviewRepository : IRepository<Review>
    {
        Task<IEnumerable<Review>> GetReviewsByMovieIdAsync(int movieId);
        Task<IEnumerable<Review>> GetReviewsByUsernameAsync(string username);
    }
}
