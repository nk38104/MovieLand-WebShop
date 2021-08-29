using MovieLand.Domain.Entities.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace MovieLand.Domain.Entities
{
    public class Genre : Entity
    {
        [Required, StringLength(64)]
        public string Name { get; set; }

        // 1-n relationships
        public List<MovieGenre> MovieGenres { get; set; }
    }
}
