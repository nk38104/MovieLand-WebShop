using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MovieLand.Application.DTOs;
using MovieLand.Application.Interfaces;


namespace MovieLand.Web.Pages.Movies
{
    public class MovieDetailModel : PageModel
    {
        private readonly IMovieService _movieService;
        private readonly IReviewService _reviewService;
        private readonly IMapper _mapper;

        public MovieDetailModel(IMovieService movieService, IReviewService reviewService, IMapper mapper)
        {
            _movieService = movieService ?? throw new ArgumentNullException(nameof(movieService));
            _reviewService = reviewService ?? throw new ArgumentNullException(nameof(reviewService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public MovieDTO Movie { get; set; } = new MovieDTO();
        [BindProperty]
        public ReviewDTO ReviewForm { get; set; } = new ReviewDTO();


        public async Task OnGetAsync(string slug)
        {
            Movie = await _movieService.GetMovieBySlug(slug);
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
