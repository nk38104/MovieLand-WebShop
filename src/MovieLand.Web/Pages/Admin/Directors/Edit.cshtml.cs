using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MovieLand.Web.Interfaces.Admin;
using MovieLand.Web.ViewModels;


namespace MovieLand.Web.Pages.Admin.Directors
{
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class EditModel : PageModel
    {
        private readonly IDirectorPageService _directorPageService;
        [BindProperty]
        public DirectorViewModel Director { get; set; }

        public EditModel(IDirectorPageService directorPageService)
        {
            _directorPageService = directorPageService ?? throw new ArgumentNullException(nameof(directorPageService));
        }


        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Director = await _directorPageService.GetDirectorById((int)id);

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
                await _directorPageService.UpdateDirector(Director);
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
            return _directorPageService.GetDirectorById(id) == null;
        }
    }
}
