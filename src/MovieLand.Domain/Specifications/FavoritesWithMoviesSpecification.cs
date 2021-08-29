using MovieLand.Domain.Entities;
using MovieLand.Domain.Specifications.Base;


namespace MovieLand.Domain.Specifications
{
    public class FavoritesWithMoviesSpecification : BaseSpecification<Favorite>
    {
        public FavoritesWithMoviesSpecification(string username)
            : base(f => f.Username.ToLower().Equals(username.ToLower()))
        {
            AddInclude(mf => mf.MovieFavorites);
        }


        public FavoritesWithMoviesSpecification(int favoritesId)
            : base(f => f.Id == favoritesId)
        {
            AddInclude(f => f.MovieFavorites);
        }
    }
}
