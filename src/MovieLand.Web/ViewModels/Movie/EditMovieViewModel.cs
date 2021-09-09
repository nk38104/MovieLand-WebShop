using MovieLand.Web.ViewModels.Directors;
using MovieLand.Web.ViewModels.Genres;
using System.Collections.Generic;


namespace MovieLand.Web.ViewModels
{
    public class EditMovieViewModel : CreateMovieViewModel
    {
        public List<MovieGenreViewModel> MovieGenres { get; set; }
        public List<MovieDirectorViewModel> MovieDirectors { get; set; }
    }
}
