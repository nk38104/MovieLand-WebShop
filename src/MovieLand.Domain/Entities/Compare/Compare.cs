﻿using MovieLand.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieLand.Domain.Entities
{
    public class Compare : Entity
    {
        public string Username { get; set; }

        public List<MovieCompare> MovieCompares { get; set; }
    }
}