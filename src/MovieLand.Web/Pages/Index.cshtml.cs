using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MovieLand.Web.Interfaces;
using MovieLand.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace MovieLand.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IIndexPageService _indexPageService;

        public IndexModel(IIndexPageService indexPageService)
        {
            _indexPageService = indexPageService ?? throw new ArgumentNullException(nameof(indexPageService));
        }

        public IEnumerable<MovieViewModel> Movies { get; set; } = new List<MovieViewModel>();
        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }

        public async Task OnGetAsync()
        {
            if (!string.IsNullOrEmpty(SearchString))
            {
                Movies = await _indexPageService.GetMoviesByTitle(SearchString);
            }
            else
            {
                Movies = await _indexPageService.GetMovies();
            }
        }
    }
}
