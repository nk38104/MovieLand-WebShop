using MovieLand.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieLand.Domain.Entities
{
    public class OrderItem : Entity
    {
        public int Quantity { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal UnitPrice { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal TotalPrice { get; set; }
        
        // 1-1 relationship
        public int MovieId { get; set; }
        public Movie Movie { get; set; }
    }
}
