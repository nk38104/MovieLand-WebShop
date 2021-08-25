using MovieLand.Application.DTOs.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieLand.Application.DTOs
{
    public class FavoritesDTO : BaseDTO
    {
        public string Username { get; set; }
        public List<MovieDTO> Movies { get; set; } = new List<MovieDTO>();
    }
}
