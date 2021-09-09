using MovieLand.Web.ViewModels.Base;
using System.Collections.Generic;


namespace MovieLand.Web.ViewModels
{
    public class MovieViewModel : BaseViewModel
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

        public List<ReviewViewModel> Reviews { get; set; }
    }
}
