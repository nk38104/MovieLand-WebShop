using MovieLand.Domain.Entities;
using MovieLand.Domain.Specifications.Base;


namespace MovieLand.Domain.Specifications
{
    public class MovieSlugSpecification : BaseSpecification<Movie>
    {
        public MovieSlugSpecification(string slug)
            : base(m => m.Slug.ToLower().Contains(slug.ToLower()))
        {
            AddInclude(m => m.Reviews);
        }
    }
}
