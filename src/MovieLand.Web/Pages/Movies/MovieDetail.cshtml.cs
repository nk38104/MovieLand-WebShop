using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MovieLand.Application.DTOs;
using MovieLand.Application.Interfaces;
using MovieLand.Web.Interfaces;
using MovieLand.Web.ViewModels;


namespace MovieLand.Web.Pages.Movies
{
    public class MovieDetailModel : PageModel
    {
        private readonly IMoviePageService _moviePageService;
        private readonly IReviewService _reviewService;
        private readonly IMapper _mapper;

        public MovieDetailModel(IMoviePageService moviePageService, IReviewService reviewService, IMapper mapper)
        {
            _moviePageService = moviePageService ?? throw new ArgumentNullException(nameof(moviePageService));
            _reviewService = reviewService ?? throw new ArgumentNullException(nameof(reviewService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public MovieViewModel Movie { get; set; } = new MovieViewModel();
        [BindProperty]
        public ReviewViewModel ReviewForm { get; set; } = new ReviewViewModel();


        public async Task OnGetAsync(string slug)
        {
            Movie = await _moviePageService.GetMovieBySlug(slug);
            TempData["Slug"] = slug;
        }
        

        public async Task<IActionResult> OnPostAddReviewToMovieAsync(int movieId, string movieSlug)
        {
            var user = User.Identity;

            if (user == null)
                return Page();

            ReviewForm.Username = user.Name;
            ReviewForm.MovieId = movieId;

            var reviewMapped = _mapper.Map<ReviewDTO>(ReviewForm);

            await _reviewService.AddReview(reviewMapped);

            return RedirectToPage("MovieDetail", new { slug = movieSlug });
        }


        public async Task<IActionResult> OnPostDeleteReviewAsync(int reviewId, string movieSlug)
        {
            if(reviewId < 1)
            {
                return NotFound();
            }

            await _reviewService.DeleteReview(reviewId);

            return RedirectToPage("MovieDetail", new { slug = movieSlug });
        }
    }
}
