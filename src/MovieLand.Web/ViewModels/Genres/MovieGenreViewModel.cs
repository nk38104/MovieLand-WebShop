

namespace MovieLand.Web.ViewModels.Genres
{
    public class MovieGenreViewModel
    {
        public int MovieId { get; set; }
        public MovieViewModel Movie { get; set; }

        public int GenreId { get; set; }
        public GenreViewModel Genre { get; set; }
    }
}
