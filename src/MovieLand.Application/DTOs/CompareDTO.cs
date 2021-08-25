using MovieLand.Application.DTOs.Base;
using System.Collections.Generic;


namespace MovieLand.Application.DTOs
{
    public class CompareDTO : BaseDTO
    {
        public string Username { get; set; }
        public List<MovieDTO> Movies { get; set; } = new List<MovieDTO>();
    }
}
