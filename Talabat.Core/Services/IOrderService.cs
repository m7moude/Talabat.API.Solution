using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Order_Aggrate;

namespace Talabat.Core.Services
{
    public interface IOrderService
    {
        Task<Order?> CreateOrderAsync(string buyerEmail, string BasketId, int deliveryId, Address address);
        Task<IReadOnlyList<Order>> GetOrderForUserAsync(string buyerEmail);
        Task<Order> GetOrderForUserByIdAsync(int id,string buyerEmail);
        Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethod();
    }
}
