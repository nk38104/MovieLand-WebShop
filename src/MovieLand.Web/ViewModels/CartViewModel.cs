using MovieLand.Web.ViewModels.Base;
using System.Collections.Generic;


namespace MovieLand.Web.ViewModels
{
    public class CartViewModel : BaseViewModel
    {
        public string Username { get; set; }
        public List<CartItemViewModel> Items { get; set; }
        public decimal GrandTotal
        {
            get
            {
                decimal grandTotal = 0;

                foreach (var item in Items)
                {
                    grandTotal += item.TotalPrice;
                }

                return grandTotal;
            }
        }
    }
}
