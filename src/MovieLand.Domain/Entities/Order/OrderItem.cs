using MovieLand.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;


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

        // n-1 relationship
        [ForeignKey("OrderId")]
        public int OrderId { get; set; }
        public virtual Order Order { get; set; }
    }
}
