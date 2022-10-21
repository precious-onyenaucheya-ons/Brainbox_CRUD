using Brainbox.Core.Interfaces;
using Brainbox.Domain.Models;
using Brainbox.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brainbox.Core.Services
{
    public class OrderService : IOrderService
    {
        private BrainboxDbContext _context;
        public OrderService(BrainboxDbContext context)
        {
            _context = context;
        }
        public async Task StoreOrderAsync(List<CartItem> items, string userId, string userEmail)
        {
            var order = new Order()
            {
                UserId = userId,
                Email = userEmail
            };
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
            foreach (var item in items)
            {
                var orderItems = new OrderItem()
                {
                    ProductId = item.Product.Id,
                    OrderId = order.Id,
                    Price = item.Product.Price,
                    Quantity = item.Quantity
                };
                await _context.OrdersItem.AddAsync(orderItems);
            }
            await _context.SaveChangesAsync();
        }
    }
}
