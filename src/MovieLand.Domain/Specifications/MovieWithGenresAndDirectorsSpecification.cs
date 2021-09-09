using MovieLand.Domain.Entities;
using MovieLand.Domain.Specifications.Base;


namespace MovieLand.Domain.Specifications
{
    public class MovieWithGenresAndDirectorsSpecification : BaseSpecification<Movie>
    {

        public MovieWithGenresAndDirectorsSpecification()
            : base(null)
        {
            AddInclude(m => m.MovieGenres);
            AddInclude(m => m.MovieDirectors);
        }


        public MovieWithGenresAndDirectorsSpecification(int movieId)
            : base(m => m.Id == movieId)
        {
            AddInclude(m => m.MovieGenres);
            AddInclude(m => m.MovieDirectors);
        }
    }
}
