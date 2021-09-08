using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MovieLand.Web.Interfaces.Admin;
using MovieLand.Web.ViewModels;


namespace MovieLand.Web.Pages.Admin.Directors
{
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class IndexModel : PageModel
    {
        private readonly IDirectorPageService _directorPageService;

        public IndexModel(IDirectorPageService directorPageService)
        {
            _directorPageService = directorPageService;
        }

        
        public IEnumerable<DirectorViewModel> Directors { get; set; } = new List<DirectorViewModel>();

        public async Task OnGetAsync()
        {
            Directors = await _directorPageService.GetDirectors();
        }
    }
}
