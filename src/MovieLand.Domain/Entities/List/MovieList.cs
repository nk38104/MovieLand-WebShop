

namespace MovieLand.Domain.Entities
{
    public class MovieList
    {
        public int MovieId { get; set; }
        public Movie Movie { get; set; }

        public int ListId { get; set; }
        public List List { get; set; }
    }
}
