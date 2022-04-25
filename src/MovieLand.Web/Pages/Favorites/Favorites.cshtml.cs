using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MovieLand.Application.DTOs;
using MovieLand.Application.Interfaces;


namespace MovieLand.Web.Pages.Favorites
{
    [Authorize]
    public class FavoritesModel : PageModel
    {
        private readonly IFavoritesService  _favoritesService;
        public FavoritesDTO Favorites { get; set; } = new FavoritesDTO();

        public FavoritesModel(IFavoritesService  favoritesService)
        {
            _favoritesService = favoritesService ?? throw new ArgumentNullException(nameof(favoritesService));
        }


        public async Task OnGetAsync()
        {
            var username = User.Identity.Name;

            Favorites = await _favoritesService.GetFavoritesByUsername(username);
        }


        public async Task<IActionResult> OnPostRemoveFromFavoritesAsync(int favoritesId, int movieId)
        {
            await _favoritesService.RemoveItem(favoritesId, movieId);

            return RedirectToPage();
        }
    }
}

