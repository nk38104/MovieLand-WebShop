using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MovieLand.Web.Interfaces.Admin;
using MovieLand.Web.ViewModels;


namespace MovieLand.Web.Pages.Admin.Genres
{
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class DeleteModel : PageModel
    {
        private readonly IGenrePageService _genrePageService;
        [BindProperty]
        public GenreViewModel Genre { get; set; }

        public DeleteModel(IGenrePageService genrePageService)
        {
            _genrePageService = genrePageService ?? throw new ArgumentNullException(nameof(genrePageService));
        }


        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
                return NotFound();

            Genre = await _genrePageService.GetGenreById((int)id);

            return (Genre == null) ? NotFound() : Page();
        }


        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
                return NotFound();

            await _genrePageService.DeleteGenre((int)id);

            return RedirectToPage("./Index");
        }
    }
}
