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
    public class CreateModel : PageModel
    {
        private readonly IGenrePageService _genrePageService;
        [BindProperty]
        public GenreViewModel Genre { get; set; }

        public CreateModel(IGenrePageService genrePageService)
        {
            _genrePageService = genrePageService ?? throw new ArgumentNullException(nameof(genrePageService));
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

            await _genrePageService.AddGenre(Genre);

            return RedirectToPage("./Index");
        }
    }
}
