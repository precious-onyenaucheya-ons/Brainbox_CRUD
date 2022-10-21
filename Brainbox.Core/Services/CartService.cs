using Brainbox.Core.DTO;
using Brainbox.Core.Interfaces;
using Brainbox.Domain.Models;
using Brainbox.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Brainbox.Core.Services
{
    public class CartService : ICartService
    {
        public BrainboxDbContext _context { get; set; }
        public string CartId { get; set; }
        public List<CartItem> CartItems { get; set; }
        public CartService(BrainboxDbContext context)
        {
            _context = context;
        }
        public static CartService GetCart(IServiceProvider provider)
        {
            ISession session = provider.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();
            session.SetString("CartId", cartId);
            return new CartService(provider.GetService<BrainboxDbContext>()) { CartId = cartId };
        }

        public void AddItemToCart(Product product)
        {
            var cartItem = _context.CartItems.FirstOrDefault(v => v.CartId == CartId
                && v.Product.Name == product.Name);
            
            if (cartItem == null)
            {
                cartItem = new CartItem
                {
                    CartId = CartId,
                    Product = product,
                    Quantity = 1,
                };
                _context.CartItems.Add(cartItem);
            }
            _context.SaveChanges();
        }

        public void RemoveItemFromCart(ProductDTO product)
        {
            var cartItem = _context.CartItems.FirstOrDefault(v => v.CartId == CartId
                && v.Product.Name== product.Name);
            if (cartItem != null)
            {
                if (cartItem.Quantity > 1)
                {
                    cartItem.Quantity--;
                }
                else
                {
                    _context.CartItems.Remove(cartItem);
                }
            }
            _context.SaveChanges();
        }

        public List<CartItem> GetCartItems() => CartItems ?? _context.CartItems.Where(v => v
        .CartId == CartId).Include(v => v.Product).ToList();

        public double CartTotal() => _context.CartItems.Where(v => v.CartId == CartId).Sum(v => v.Product.Price * v.Quantity);

        public async Task ClearCartAsync()
        {
            var anime = await _context.CartItems.Where(v => v.CartId == CartId).ToListAsync();
            _context.CartItems.RemoveRange(anime);
            await _context.SaveChangesAsync();
        }
    }
}
