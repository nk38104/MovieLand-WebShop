using MovieLand.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace MovieLand.Application.Interfaces
{
    public interface IReviewService
    {
        Task AddReview(ReviewDTO review);
        Task DeleteReview(int reviewId);

        Task<IEnumerable<ReviewDTO>> GetReviewsByMovieId(int movieId);
        Task<IEnumerable<ReviewDTO>> GetReviewsByUsername(string username);
    }
}
