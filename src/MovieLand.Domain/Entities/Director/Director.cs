using MovieLand.Domain.Entities.Base;
using System.Collections.Generic;


namespace MovieLand.Domain.Entities
{
    public class Director : Entity
    {
        public string Name { get; set; }

        // 1-n relationships
        public List<MovieDirector> MovieDirectors { get; set; }
    }
}
