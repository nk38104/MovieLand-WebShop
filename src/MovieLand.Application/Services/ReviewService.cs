using MovieLand.Application.DTOs;
using MovieLand.Application.Interfaces;
using MovieLand.Application.Mapper;
using MovieLand.Domain.Entities;
using MovieLand.Domain.Interfaces;
using MovieLand.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace MovieLand.Application.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IMovieRepository _movieRepository;
        private readonly IAppLogger<ReviewService> _logger;

        public ReviewService(IReviewRepository reviewRepository, IMovieRepository movieRepository, IAppLogger<ReviewService> logger)
        {
            _reviewRepository = reviewRepository ?? throw new ArgumentNullException(nameof(reviewRepository));
            _movieRepository = movieRepository ?? throw new ArgumentNullException(nameof(movieRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }


        public async Task AddReview(ReviewDTO review)
        {
            var movie = await _movieRepository.GetByIdAsync(review.MovieId);
            var reviewMapped = ObjectMapper.Mapper.Map<Review>(review);
            var newReview = await _reviewRepository.AddAsync(reviewMapped);

            movie.AddReview(newReview);

            await _movieRepository.UpdateAsync(movie);
        }


        public async Task DeleteReview(int reviewId)
        {
            var review = await _reviewRepository.GetByIdAsync(reviewId);

            await _reviewRepository.DeleteReviewAsync(review);
        }


        public async Task RemoveReview(int reviewId, int movieId)
        {
            var movie = await _movieRepository.GetByIdAsync(movieId);
            await _reviewRepository.DeleteAsync(_reviewRepository.GetByIdAsync(reviewId).Result);

            movie.RemoveReview(reviewId);

            await _movieRepository.UpdateAsync(movie);
        }


        public async Task<IEnumerable<ReviewDTO>> GetReviewsByMovieId(int movieId)
        {
            var movieReviews = await _reviewRepository.GetReviewsByMovieIdAsync(movieId);
            var movieReviewsMapped = ObjectMapper.Mapper.Map<IEnumerable<ReviewDTO>>(movieReviews);

            return movieReviewsMapped;
        }


        public async Task<IEnumerable<ReviewDTO>> GetReviewsByUsername(string username)
        {
            var userReviews = await _reviewRepository.GetReviewsByUsernameAsync(username);
            var userReviewsMapped = ObjectMapper.Mapper.Map<IEnumerable<ReviewDTO>>(userReviews);

            return userReviewsMapped;
        }
    }
}
