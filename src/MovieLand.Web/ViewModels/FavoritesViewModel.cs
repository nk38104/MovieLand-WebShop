using MovieLand.Web.ViewModels.Base;
using System.Collections.Generic;


namespace MovieLand.Web.ViewModels
{
    public class FavoritesViewModel : BaseViewModel
    {
        public string Username { get; set; }
        public List<MovieViewModel> Movies { get; set; } = new List<MovieViewModel>();
    }
}
