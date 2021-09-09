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
    public class EditModel : PageModel
    {
        private readonly IGenrePageService _genrePageService;
        [BindProperty]
        public GenreViewModel Genre { get; set; }

        public EditModel(IGenrePageService genrePageService)
        {
            _genrePageService = genrePageService ?? throw new ArgumentNullException(nameof(genrePageService));
        }


        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Genre = await _genrePageService.GetGenreById((int)id);

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
                await _genrePageService.UpdateGenre(Genre);
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
            return _genrePageService.GetGenreById(id) == null;
        }
    }
}
