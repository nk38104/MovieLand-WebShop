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
    public class EditModel : PageModel
    {
        private readonly IGenreService _genreService;
        [BindProperty]
        public GenreDTO Genre { get; set; }

        public EditModel(IGenreService genreService)
        {
            _genreService = genreService ?? throw new ArgumentNullException(nameof(genreService));
        }


        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Genre = await _genreService.GetGenreById((int)id);

            return (Genre == null) ? NotFound() : Page();
        }


        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                await _genreService.UpdateGenre(Genre);
            }
            catch (Exception)
            {
                if (!GenreExists(Genre.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }


        private bool GenreExists(int id)
        {
            return _genreService.GetGenreById(id) == null;
        }
    }
}
