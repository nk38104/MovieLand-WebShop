

namespace MovieLand.Application.DTOs.Genre
{
    public class MovieGenreDTO
    {
        public int MovieId { get; set; }
        public MovieDTO Movie { get; set; }

        public int GenreId { get; set; }
        public GenreDTO Genre { get; set; }
    }
}
