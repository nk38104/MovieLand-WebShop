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
    public class CreateModel : PageModel
    {
        private readonly IGenreService _genreService;
        [BindProperty]
        public GenreDTO Genre { get; set; }

        public CreateModel(IGenreService genreService)
        {
            _genreService = genreService ?? throw new ArgumentNullException(nameof(genreService));
        }


        public IActionResult OnGet()
        {
            return Page();
        }


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _genreService.AddGenre(Genre);

            return RedirectToPage("./Index");
        }
    }
}
