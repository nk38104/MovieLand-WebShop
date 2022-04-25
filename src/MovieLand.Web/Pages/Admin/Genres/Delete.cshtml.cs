using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MovieLand.Application.DTOs;
using MovieLand.Application.Interfaces;

namespace MovieLand.Web.Pages.Admin.Genres
{
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class DeleteModel : PageModel
    {
        private readonly IGenreService _genreService;
        [BindProperty]
        public GenreDTO Genre { get; set; }

        public DeleteModel(IGenreService genreService)
        {
            _genreService = genreService ?? throw new ArgumentNullException(nameof(genreService));
        }


        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
                return NotFound();

            Genre = await _genreService.GetGenreById((int)id);

            return (Genre == null) ? NotFound() : Page();
        }


        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
                return NotFound();

            await _genreService.DeleteGenre((int)id);

            return RedirectToPage("./Index");
        }
    }
}
