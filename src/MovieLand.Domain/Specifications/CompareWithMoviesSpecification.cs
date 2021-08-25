using MovieLand.Domain.Entities;
using MovieLand.Domain.Specifications.Base;


namespace MovieLand.Domain.Specifications
{
    public class CompareWithMoviesSpecification : BaseSpecification<Compare>
    {
        public CompareWithMoviesSpecification(string username)
            : base(mc => mc.Username.ToLower() == username.ToLower())
        {
            AddInclude(mc => mc.MovieCompares);
        }


        public CompareWithMoviesSpecification(int compareId)
           : base(mc => mc.Id == compareId)
        {
            AddInclude(mc => mc.MovieCompares);
        }
    }
}
