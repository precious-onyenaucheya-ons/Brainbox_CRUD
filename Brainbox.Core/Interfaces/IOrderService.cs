using Brainbox.Domain.Models;

namespace Brainbox.Core.Interfaces
{
    public interface IOrderService
    {
        Task StoreOrderAsync(List<CartItem> items, string userId, string userEmail);
    }
}