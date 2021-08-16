using MovieLand.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieLand.Domain.Entities
{
    public class Genre : Entity
    {
        [Required, StringLength(64)]
        public string Name { get; set; }
    }
}
