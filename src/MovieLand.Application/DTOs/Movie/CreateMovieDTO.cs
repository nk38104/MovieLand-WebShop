using MovieLand.Application.DTOs.Base;
using System.ComponentModel.DataAnnotations;


namespace MovieLand.Application.DTOs
{
    public class CreateMovieDTO : BaseDTO
    {
        public string Title { get; set; }
        public string Slug { get; set; }
        public string PictureUri { get; set; }
        public string ReleaseYear { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        public string Duration { get; set; }
        public decimal UnitPrice { get; set; } = 0;
        public int? UnitsInStock { get; set; } = 0;
        public double Rate { get; set; } = 0;
    }
}
