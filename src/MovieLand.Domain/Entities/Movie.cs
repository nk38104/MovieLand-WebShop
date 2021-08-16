using MovieLand.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieLand.Domain.Entities
{
    public class Movie : Entity
    {
        [Required, StringLength(128)]
        public string Title { get; set; }
        public DateTime ReleaseYear { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        public string Duration { get; set; }
        public string Slug { get; set; }
        public decimal UnitPrice { get; set; }
        public int? UnitsInStock { get; set; }
        public string PictureUri { get; set; }
        public double Rate { get; set; }

        // 1-n relationships
        public List<Review> Reviews { get; set; }

        // n-n relationships
        public List<MovieGenre> MovieGenres { get; set; }
        public List<MovieDirector> MovieDirectors { get; set; }
        public List<MovieFavorite> MovieFavorites { get; set; }
        public List<MovieCompare> MovieCompares{ get; set; }
        public List<MovieList> MovieLists { get; set; }
    }
}
