using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MovieLand.Application.DTOs;
using MovieLand.Application.Interfaces;


namespace MovieLand.Web.Pages.Admin.Movies
{
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class DeleteModel : PageModel
    {
        private readonly IMovieService _movieService;
        
        [BindProperty]
        public MovieDTO Movie { get; set; }
        public string RequestPagePath { get; set; }

        public DeleteModel(IMovieService movieService)
        {
            _movieService = movieService ?? throw new ArgumentNullException(nameof(movieService));
        }

        
        public async Task<IActionResult> OnGetAsync(int? movieId, string requestPagePath)
        {
            if (movieId == null || movieId < 1)
            {
                return NotFound();
            }

            Movie = await _movieService.GetMovieById((int)movieId);
            RequestPagePath = (requestPagePath == "/") ? "/Index" : requestPagePath;

            return (Movie == null) ? NotFound() : Page();
        }


        public async Task<IActionResult> OnPostAsync(int? movieId, string requestPagePath)
        {
            if (movieId == null || movieId < 1)
            {
                return NotFound();
            }

            await _movieService.DeleteMovie((int)movieId);

            return Redirect(requestPagePath);
        }
    }
}
