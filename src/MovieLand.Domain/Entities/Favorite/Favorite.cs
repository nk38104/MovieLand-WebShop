using MovieLand.Domain.Entities.Base;
using System.Collections.Generic;
using System.Linq;


namespace MovieLand.Domain.Entities
{
    public class Favorite : Entity
    {
        public string Username { get; set; }

        // 1-n relationships
        public List<MovieFavorite> MovieFavorites { get; set; }


        public void AddItem(int movieId)
        {
            var existingItem = MovieFavorites.FirstOrDefault(m => m.MovieId == movieId);
            
            if (existingItem != null)
                return;

            MovieFavorites.Add(new MovieFavorite
            {
                MovieId = movieId,
                FavoriteId = this.Id
            });
        }


        public void RemoveItem(int movieId)
        {
            var movieToRemove = MovieFavorites.FirstOrDefault(m => m.MovieId == movieId);
        
            if (movieToRemove != null)
            {
                MovieFavorites.Remove(movieToRemove);
            }
        }
    }
}
