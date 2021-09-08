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
    public class DeleteModel : PageModel
    {
        private readonly IDirectorPageService _directorPageService;
        [BindProperty]
        public DirectorViewModel Director { get; set; }

        public DeleteModel(IDirectorPageService directorPageService)
        {
            _directorPageService = directorPageService ?? throw new ArgumentNullException(nameof(directorPageService));
        }


        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
                return NotFound();

            Director = await _directorPageService.GetDirectorById((int)id);

            return (Director == null) ? NotFound() : Page();
        }
        

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
                return NotFound();

            await _directorPageService.DeleteDirector((int)id);

            return RedirectToPage("./Index");
        }
    }
}
