using MovieLand.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieLand.Domain.Entities
{
    public class Director : Entity
    {
        public string Name { get; set; }

        // 1-n relationships
        public List<MovieDirector> MovieDirectors { get; set; }
    }
}
