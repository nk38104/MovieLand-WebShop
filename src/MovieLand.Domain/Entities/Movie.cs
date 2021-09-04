using MovieLand.Domain.Entities.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;


namespace MovieLand.Domain.Entities
{
    public class Movie : Entity
    {
        [Required, StringLength(128)]
        public string Title { get; set; }
        public string Slug { get; set; }
        public string PictureUri { get; set; }
        public string ReleaseYear { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        public string Duration { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal UnitPrice { get; set; }
        public int? UnitsInStock { get; set; }
        public double Rate { get; set; }

        // 1-n relationships
        public List<Review> Reviews { get; set; }

        // n-n relationships
        public List<MovieGenre> MovieGenres { get; set; }
        public List<MovieDirector> MovieDirectors { get; set; }
        public List<MovieFavorite> MovieFavorites { get; set; }
        public List<MovieCompare> MovieCompares{ get; set; }
        public List<MovieList> MovieLists { get; set; }


        public void AddReview(Review review)
        {
            if(review != null)
                Reviews.Add(review);
        }


        public void RemoveReview(int reviewId)
        {
            //var reviewToRemove = Reviews.Where(r => r.Id == reviewId).FirstOrDefault();
            //Reviews.Remove(reviewToRemove);

            Reviews.RemoveAll(r => r.Id == reviewId);
        }
    }
}
