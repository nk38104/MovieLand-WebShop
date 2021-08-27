using MovieLand.Application.DTOs.Base;
using MovieLand.Application.DTOs.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieLand.Application.DTOs
{
    public class OrderDTO : BaseDTO
    {
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string ContactNumber { get; set; }
        public string ShippingAddress { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public decimal GrandTotal { get; set; }
        public PaymentMethodEnumDTO PaymentMethod { get; set; } = PaymentMethodEnumDTO.Cash;
        public OrderStatusEnumDTO Status { get; set; } = OrderStatusEnumDTO.InProgress;
        public DateTime DateOrdered { get; set; } = DateTime.Now;

        public List<OrderItemDTO> Items { get; set; } = new List<OrderItemDTO>();
    }
}
