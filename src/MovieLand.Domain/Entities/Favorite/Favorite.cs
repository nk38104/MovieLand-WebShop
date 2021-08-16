using MovieLand.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieLand.Domain.Entities
{
    public class Favorite : Entity
    {
        public string Username { get; set; }

        public List<MovieFavorite> MovieFavorites { get; set; }
    }
}
