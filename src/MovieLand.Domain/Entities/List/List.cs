using MovieLand.Domain.Entities.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace MovieLand.Domain.Entities
{
    public class List : Entity
    {
        [Required, StringLength(64)]
        public string Name { get; set; }
        public string Description { get; set; }
        public string PictureUri { get; set; }

        // 1-n relationships
        public List<MovieList> MovieLists { get; set; }
    }
}
