using MovieLand.Domain.Entities;
using MovieLand.Domain.Interfaces.Repositories;
using MovieLand.Infrastructure.Data;
using MovieLand.Infrastructure.Repositories.Base;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace MovieLand.Infrastructure.Repositories
{
    public class ReviewRepository : Repository<Review>, IReviewRepository
    {
        public ReviewRepository(MovieLandContext movieLandContext)
            : base(movieLandContext) { }


        public async Task<IEnumerable<Review>> GetReviewsByMovieIdAsync(int movieId)
        {
            return await GetAsync(r => r.MovieId == movieId);
        }


        public async Task<IEnumerable<Review>> GetReviewsByUsernameAsync(string username)
        {
            return await GetAsync(r => r.Username.ToLower() == username.ToLower());
        }
    }
}
