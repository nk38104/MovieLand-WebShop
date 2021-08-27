using MovieLand.Web.ViewModels.Base;
using MovieLand.Web.ViewModels.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace MovieLand.Web.ViewModels
{
    public class OrderViewModel : BaseViewModel
    {
        public string Username { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string ContactNumber { get; set; }
        [Required]
        public string ShippingAddress { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string PostalCode { get; set; }
        [Required]
        public string Country { get; set; }

        public PaymentMethodEnumViewModel PaymentMethod { get; set; } = PaymentMethodEnumViewModel.Cash;
        public OrderStatusEnumViewModel Status { get; set; } = OrderStatusEnumViewModel.InProgress;
        public DateTime DateOrdered { get; set; } = DateTime.Now;

        public List<OrderItemViewModel> Items { get; set; } = new List<OrderItemViewModel>();
        
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
