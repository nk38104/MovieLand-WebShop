using MovieLand.Web.ViewModels.Base;


namespace MovieLand.Web.ViewModels
{
    public class OrderItemViewModel : BaseViewModel
    {
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }

        public int MovieId { get; set; }
        public MovieViewModel Movie { get; set; }
    }
}
