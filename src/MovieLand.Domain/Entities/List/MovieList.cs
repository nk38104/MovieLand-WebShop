﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieLand.Domain.Entities
{
    public class MovieList
    {
        public int MovieId { get; set; }
        public Movie Movie { get; set; }

        public int ListId { get; set; }
        public List List { get; set; }
    }
}
