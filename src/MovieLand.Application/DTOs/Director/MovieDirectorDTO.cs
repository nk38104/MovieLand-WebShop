

namespace MovieLand.Application.DTOs.Director
{
    public class MovieDirectorDTO
    {
        public int MovieId { get; set; }
        public MovieDTO Movie { get; set; }

        public int DirectorId { get; set; }
        public DirectorDTO Director { get; set; }
    }
}