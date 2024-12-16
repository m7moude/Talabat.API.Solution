using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Order_Aggrate;
using Talabat.Core.IRepositories;
using Talabat.Core.Services;
using Talabat.Core.Specifications;

namespace Talabat.Services
{
    public class OrderServices : IOrderService
    {
        private readonly IBasketRepository _basketRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPaymentServices _paymentServices;

        public OrderServices(IBasketRepository basketRepo,IUnitOfWork unitOfWork,IPaymentServices paymentServices) 
        {
            _basketRepo = basketRepo;
            _unitOfWork = unitOfWork;
            _paymentServices = paymentServices;
        }
        public async Task<Order?> CreateOrderAsync(string buyerEmail, string BasketId, int deliveryId, Address address)
        {
            //1. Get Basket From Basket Repo
            var basket = await _basketRepo.GetBasketAsync(BasketId);

            //2. Get Selected Items at Basket From Products Repo            
            var basketItems= new List<OrderItem>();
            if (basket?.Items?.Count>0)
            {
                foreach (var item in basket.Items)
                {
                    var product = await _unitOfWork.Repositories<Product>().GetByIdAsync(item.Id);
                    var productItemOrder = new ProductOrderItem(item.Id, product.Name, product.UrlPicture);
                    var orderItem = new OrderItem(productItemOrder, product.Price, item.Quantity);
                    
                    basketItems.Add(orderItem);
                }
            }

            //3. Calculate Sub Total (Price * Quantity)
            var subTotal = basketItems.Sum(items => items.Quntity * items.Price);

            //4. Get Delivery Method from Delivery Method Repo
            var delivery = await _unitOfWork.Repositories<DeliveryMethod>().GetByIdAsync(deliveryId);

            //5. Create Order
            var spec = new OrderWithPaymentIntentIdSpec(basket.PaymentIntentId);
            var existOrder = await _unitOfWork.Repositories<Order>().GetByIdWithSpecAsync(spec);
            if (existOrder is not null)
            {
                _unitOfWork.Repositories<Order>().Delete(existOrder);
                await _paymentServices.CreateOrUpdatePaymentIntent(basket.Id);
            }
            var order = new Order(buyerEmail, address, delivery, subTotal, basketItems, basket.PaymentIntentId);
            await _unitOfWork.Repositories<Order>().Add(order);
            //6. Save to Datebase
            var result =await _unitOfWork.Complet();
            return result <= 0 ? null : order; 
        }

        
        public async Task<IReadOnlyList<Order>> GetOrderForUserAsync(string buyerEmail)
        {
            var spec = new OrderSpecification(buyerEmail);
            var orders = await _unitOfWork.Repositories<Order>().GetAllWithSpecAsync(spec);
            return orders;
        }

        public async Task<Order> GetOrderForUserByIdAsync(int id, string buyerEmail)
        {
            var spec =new OrderSpecification(id,buyerEmail);
            var order = await _unitOfWork.Repositories<Order>().GetByIdWithSpecAsync(spec);
            return order;
        }

        public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethod()
        {
            return await _unitOfWork.Repositories<DeliveryMethod>().GetAllAsync();
        }

    }
}
