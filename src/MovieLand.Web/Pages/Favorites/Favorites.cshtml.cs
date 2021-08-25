using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MovieLand.Web.Interfaces;
using MovieLand.Web.ViewModels;

namespace MovieLand.Web.Pages.Favorites
{
    //[Authorize]
    public class FavoritesModel : PageModel
    {
        private readonly IFavoritesPageService _favoritesPageService;
        public FavoritesViewModel Favorites { get; set; } = new FavoritesViewModel();

        public FavoritesModel(IFavoritesPageService favoritesPageService)
        {
            _favoritesPageService = favoritesPageService ?? throw new ArgumentNullException(nameof(favoritesPageService));
        }


        public async Task OnGetAsync()
        {
            var username = this.User.Identity.Name;

            if(username == null)
            {
                username = "bg123";
            }

            Favorites = await _favoritesPageService.GetFavorites(username);
        }


        public async Task<IActionResult> OnPostRemoveFromFavoritesAsync(int favoritesId, int movieId)
        {
            await _favoritesPageService.RemoveMovie(favoritesId, movieId);
            return RedirectToPage();
        }
    }
}

