

namespace MovieLand.Web.ViewModels.Directors
{
    public class MovieDirectorViewModel
    {
        public int MovieId { get; set; }
        public MovieViewModel Movie { get; set; }

        public int DirectorId { get; set; }
        public DirectorViewModel Director { get; set; }
    }
}
