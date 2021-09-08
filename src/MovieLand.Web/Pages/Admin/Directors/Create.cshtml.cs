using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MovieLand.Web.Interfaces.Admin;
using MovieLand.Web.ViewModels;


namespace MovieLand.Web.Pages.Admin.Directors
{
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class CreateModel : PageModel
    {
        private readonly IDirectorPageService _directorPageService;
        [BindProperty]
        public DirectorViewModel Director { get; set; }

        public CreateModel(IDirectorPageService directorPageService)
        {
            _directorPageService = directorPageService ?? throw new ArgumentNullException(nameof(directorPageService));
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

            await _directorPageService.AddDirector(Director);

            return RedirectToPage("./Index");
        }
    }
}
