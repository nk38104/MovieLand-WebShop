using MovieLand.Application.DTOs.Base;
using System.Collections.Generic;

namespace MovieLand.Application.DTOs
{
    public class MovieDTO : BaseDTO
    {
        public string Title { get; set; }
        public string Slug { get; set; }
        public string PictureUri { get; set; }
        public string ReleaseYear { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        public string Duration { get; set; }
        public decimal UnitPrice { get; set; }
        public int? UnitsInStock { get; set; }
        public double Rate { get; set; }

        public List<ReviewDTO> Reviews { get; set; }
    }
}
