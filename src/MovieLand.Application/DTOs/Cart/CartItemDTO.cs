using MovieLand.Application.DTOs.Base;


namespace MovieLand.Application.DTOs
{
    public class CartItemDTO : BaseDTO
    {
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }

        public int MovieId { get; set; }
        public MovieDTO Movie { get; set; }
    }
}
