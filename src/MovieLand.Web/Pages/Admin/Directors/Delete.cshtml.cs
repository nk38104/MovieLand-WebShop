using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MovieLand.Application.DTOs;
using MovieLand.Application.Interfaces;


namespace MovieLand.Web.Pages.Admin.Directors
{
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class DeleteModel : PageModel
    {
        private readonly IDirectorService _directorService;
        [BindProperty]
        public DirectorDTO Director { get; set; }

        public DeleteModel(IDirectorService directorService)
        {
            _directorService = directorService ?? throw new ArgumentNullException(nameof(directorService));
        }


        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
                return NotFound();

            Director = await _directorService.GetDirectorById((int)id);

            return (Director == null) ? NotFound() : Page();
        }
        

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
                return NotFound();

            await _directorService.DeleteDirector((int)id);

            return RedirectToPage("./Index");
        }
    }
}
