using MovieLand.Domain.Entities.Base;
using System.Collections.Generic;
using System.Linq;


namespace MovieLand.Domain.Entities
{
    public class Compare : Entity
    {
        public string Username { get; set; }
        
        // 1-n relationships
        public List<MovieCompare> MovieCompares { get; set; }


        public void AddItem(int movieId)
        {
            var existingMovie = MovieCompares.FirstOrDefault(mc => mc.MovieId == movieId);
            
            if (existingMovie != null)
                return;

            MovieCompares.Add( new MovieCompare
            {
                MovieId = movieId,
                CompareId = this.Id
            });
        }


        public void RemoveItem(int movieId)
        {
            var removedItem = MovieCompares.FirstOrDefault(mc => mc.MovieId == movieId);
            
            if (removedItem != null)
            {
                MovieCompares.Remove(removedItem);
            }
        }
    }
}
