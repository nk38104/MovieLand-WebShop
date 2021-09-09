using MovieLand.Web.ViewModels.Base;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace MovieLand.Web.ViewModels
{
    public class CreateMovieViewModel : BaseViewModel
    {
        [Required, StringLength(128)]
        public string Title { get; set; }
        [Required]
        public string Slug { get; set; }
        [DisplayName("Picture URL")]
        public string PictureUri { get; set; }
        [Required, DisplayName("Release year")]
        public string ReleaseYear { get; set; }
        [Required]
        public string Summary { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Duration { get; set; }
        [Required , Column(TypeName = "decimal(18, 2)"), DisplayName("Price")]
        public decimal UnitPrice { get; set; } = 0;
        [DisplayName("Units in stock")]
        public int? UnitsInStock { get; set; } = 0;
        public double Rate { get; set; } = 0;
    }
}
