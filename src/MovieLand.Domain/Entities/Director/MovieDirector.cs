﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieLand.Domain.Entities
{
    public class MovieDirectors
    {
        public int MovieId { get; set; }
        public Movie Movie { get; set; }

        public int DirectorId { get; set; }
        public Director Director { get; set; }
    }
}
