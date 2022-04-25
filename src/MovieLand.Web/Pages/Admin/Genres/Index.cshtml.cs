using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MovieLand.Application.DTOs;
using MovieLand.Application.Interfaces;


namespace MovieLand.Web.Pages.Admin.Genres
{
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class IndexModel : PageModel
    {
        private readonly IGenreService _genresPageService;
        public IEnumerable<GenreDTO> Genres { get; set; } = new List<GenreDTO>();

        public IndexModel(IGenreService genresPageService)
        {
            _genresPageService = genresPageService ?? throw new ArgumentNullException(nameof(genresPageService));
        }


        public async Task OnGetAsync()
        {
            Genres = await _genresPageService.GetGenreList();
        }
    }
}
