using Brainbox.Core.DTO;
using Brainbox.Domain.Models;
using Brainbox.Infrastructure;

namespace Brainbox.Core.Interfaces
{
    public interface ICartService
    {
        BrainboxDbContext _context { get; set; }
        string CartId { get; set; }
        List<CartItem> CartItems { get; set; }

        void AddItemToCart(Product product);
        double CartTotal();
        Task ClearCartAsync();
        List<CartItem> GetCartItems();
        void RemoveItemFromCart(ProductDTO product);
    }
}