using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MovieLand.Web.Interfaces.Admin;
using MovieLand.Web.ViewModels;


namespace MovieLand.Web.Pages.Admin.Genres
{
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class IndexModel : PageModel
    {
        private readonly IGenrePageService _genresPageService;
        public IEnumerable<GenreViewModel> Genres { get; set; } = new List<GenreViewModel>();

        public IndexModel(IGenrePageService genresPageService)
        {
            _genresPageService = genresPageService ?? throw new ArgumentNullException(nameof(genresPageService));
        }


        public async Task OnGetAsync()
        {
            Genres = await _genresPageService.GetGenres();
        }
    }
}
