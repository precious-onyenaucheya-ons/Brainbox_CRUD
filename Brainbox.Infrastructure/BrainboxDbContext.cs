using Brainbox.Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Brainbox.Infrastructure
{
    public class BrainboxDbContext: IdentityDbContext<Customer>
    {
        public BrainboxDbContext(DbContextOptions<BrainboxDbContext> options): base(options)
        {

        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrdersItem { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
    }
}
