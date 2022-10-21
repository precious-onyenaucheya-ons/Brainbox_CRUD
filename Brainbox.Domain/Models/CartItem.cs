using System.ComponentModel.DataAnnotations;

namespace Brainbox.Domain.Models
{
    public class CartItem
    {
        [Key]
        public int Id { get; set; }

        public string CartId { get; set; }

        public int Quantity { get; set; }

        public Product Product { get; set; }
    }
}
