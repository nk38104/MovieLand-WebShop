

namespace MovieLand.Domain.Entities
{
    public class MovieCompare
    {
        public int MovieId { get; set; }
        public Movie Movie { get; set; }

        public int CompareId { get; set; }
        public Compare Compare { get; set; }
    }
}
