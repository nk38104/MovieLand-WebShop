using MovieLand.Domain.Entities;
using MovieLand.Domain.Specifications.Base;


namespace MovieLand.Domain.Specifications
{
    public class MovieWithGenresSpecification : BaseSpecification<Movie>
    {
        public MovieWithGenresSpecification()
            : base(null)
        {
            AddInclude(m => m.MovieGenres);
        }

        public MovieWithGenresSpecification(string title)
            : base(m => m.Title.ToLower() == title.ToLower())
        {
            AddInclude(m => m.MovieGenres);
        }

        public MovieWithGenresSpecification(int movieId)
            : base(m => m.Id == movieId)
        {
            AddInclude(m => m.MovieGenres);
        }
    }
}
