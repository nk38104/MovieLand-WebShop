

namespace MovieLand.Domain.Entities
{
    public class MovieFavorite
    {
        public int MovieId { get; set; }
        public Movie Movie { get; set; }

        public int FavoriteId { get; set; }
        public Favorite Favorite { get; set; }
    }
}
