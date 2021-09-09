using MovieLand.Domain.Entities;
using MovieLand.Domain.Specifications.Base;


namespace MovieLand.Domain.Specifications
{
    public class CreateMovieSpecification : BaseSpecification<Movie>
    {

        public CreateMovieSpecification()
            : base(null)
        {
            AddInclude(m => m.MovieGenres);
            AddInclude(m => m.MovieDirectors);
        }
    }
}
