using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MovieLand.Application.DTOs;
using MovieLand.Application.Interfaces;


namespace MovieLand.Web.Pages.Admin.Directors
{
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class IndexModel : PageModel
    {
        private readonly IDirectorService _directorService;

        public IndexModel(IDirectorService directorService)
        {
            _directorService = directorService ?? throw new ArgumentNullException(nameof(directorService));
        }

        
        public IEnumerable<DirectorDTO> Directors { get; set; } = new List<DirectorDTO>();

        public async Task OnGetAsync()
        {
            Directors = await _directorService.GetDirectorList();
        }
    }
}
