using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MovieLand.Web.Interfaces;
using MovieLand.Web.ViewModels;


namespace MovieLand.Web.Pages.Admin.Movies
{
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class DeleteModel : PageModel
    {
        private readonly IMoviePageService _moviePageService;
        [BindProperty]
        public MovieViewModel Movie { get; set; }

        public DeleteModel(IMoviePageService moviePageService)
        {
            _moviePageService = moviePageService ?? throw new ArgumentNullException(nameof(moviePageService));
        }

        
        public async Task<IActionResult> OnGetAsync(int? movieId)
        {
            if (movieId == null || movieId < 1)
            {
                return NotFound();
            }

            Movie = await _moviePageService.GetMovieById((int)movieId);

            return (Movie == null) ? NotFound() : Page();
        }


        public async Task<IActionResult> OnPostAsync(int? movieId)
        {
            if (movieId == null || movieId < 1)
            {
                return NotFound();
            }

            await _moviePageService.DeleteMovie((int)movieId);

            return RedirectToPage("../../Index");
        }
    }
}
