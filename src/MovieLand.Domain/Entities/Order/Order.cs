using MovieLand.Domain.Entities.Base;
using MovieLand.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;


namespace MovieLand.Domain.Entities
{
    public class Order : Entity
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
        public PaymentMethod PaymentMethod { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime DateOrdered { get; set; }
        public DateTime ShippingDate { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal GrandTotal { get; set; }

        // 1-n relationships
        public virtual List<OrderItem> Items { get; set; }
    }
}
