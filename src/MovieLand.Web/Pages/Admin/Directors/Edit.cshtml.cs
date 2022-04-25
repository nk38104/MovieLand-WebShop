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
    public class EditModel : PageModel
    {
        private readonly IDirectorService _directorService;
        [BindProperty]
        public DirectorDTO Director { get; set; }

        public EditModel(IDirectorService directorService)
        {
            _directorService = directorService ?? throw new ArgumentNullException(nameof(directorService));
        }


        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Director = await _directorService.GetDirectorById((int)id);

            return (Director == null) ? NotFound() : Page();
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
                await _directorService.UpdateDirector(Director);
            }
            catch (Exception)
            {
                if (!DirectorExists(Director.Id))
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


        private bool DirectorExists(int id)
        {
            return _directorService.GetDirectorById(id) != null;
        }
    }
}
