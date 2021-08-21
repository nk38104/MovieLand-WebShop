using MovieLand.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieLand.Domain.Entities
{
    public class Review : Entity
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Comment { get; set; }
        public double Rate { get; set; }

        //// 1-n relationships
        //public int MovieId { get; set; }
        //public Movie Movie { get; set; }
    }
}
