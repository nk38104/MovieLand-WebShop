using MovieLand.Domain.Entities.Base;
using System.Collections.Generic;
using System.Linq;


namespace MovieLand.Domain.Entities
{
    public class Cart : Entity
    {
        public string Username { get; set; }

        // 1-n relationships
        public List<CartItem> Items { get; set; }


        public void AddItem(int movieId, int quantity = 1, decimal unitPrice = 0)
        {
            var existingItem = Items.FirstOrDefault(i => i.MovieId == movieId);

            if (existingItem != null)
            {
                existingItem.Quantity++;
                existingItem.TotalPrice = existingItem.Quantity * existingItem.UnitPrice;
            }
            else
            {
                Items.Add(
                    new CartItem()
                    {
                        MovieId = movieId,
                        Quantity = quantity,
                        UnitPrice = unitPrice,
                        TotalPrice = quantity * unitPrice
                    });
            }
        }


        public void RemoveItem(int cartItemId)
        {
            var removedItem = Items.FirstOrDefault(i => i.Id == cartItemId);

            if (removedItem != null)
            {
                Items.Remove(removedItem);
            }
        }


        public void RemoveItemWithMovie(int movieId)
        {
            var removedItem = Items.FirstOrDefault(i => i.MovieId == movieId);

            if (removedItem != null)
            {
                Items.Remove(removedItem);
            }
        }


        public void ClearItems()
        {
            Items.Clear();
        }
    }
}
