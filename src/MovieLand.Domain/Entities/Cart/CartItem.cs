using MovieLand.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;


namespace MovieLand.Domain.Entities
{
    public class CartItem : Entity
    {
        public int Quantity { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal UnitPrice { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal TotalPrice { get; set; }

        // 1-1 relationships
        public int MovieId { get; set; }
        public Movie Movie { get; set; }
    }
}
