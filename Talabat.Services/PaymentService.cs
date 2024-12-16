using Microsoft.Extensions.Configuration;
using Stripe;
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
using Product = Talabat.Core.Entities.Product;

namespace Talabat.Services
{
    public class PaymentService : IPaymentServices
    {
        private readonly IConfiguration _configuration;
        private readonly IBasketRepository _basketRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PaymentService(IConfiguration configuration,IBasketRepository basketRepository,IUnitOfWork unitOfWork)
        {
            _configuration = configuration;
            _basketRepository = basketRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<CustomerBasket> CreateOrUpdatePaymentIntent(string basketId)
        {
            StripeConfiguration.ApiKey = _configuration["StripePayments:Secretkey"];
            var basket =await _basketRepository.GetBasketAsync(basketId);
            if (basket is null) return null;
            decimal shippingPrice = 0m;
            if (basket.DeliveryMethodId.HasValue)
            {
                var delivaryMethod=await _unitOfWork.Repositories<DeliveryMethod>().GetByIdAsync(basket.DeliveryMethodId.Value);
                basket.ShippingCost = delivaryMethod.Cost;
                shippingPrice = delivaryMethod.Cost;

            }

            if (basket?.Items?.Count>0)
            {
                foreach (var item in basket.Items)
                {
                    var product =await _unitOfWork.Repositories<Product>().GetByIdAsync(item.Id);
                    if(item.price!=product.Price)
                        item.price = product.Price;
                }
            }

            var service = new PaymentIntentService();
            PaymentIntent paymentIntent;
            if (string.IsNullOrEmpty(basket.PaymentIntentId))
            {
                var options = new PaymentIntentCreateOptions()
                {
                    Amount = (long)basket.Items.Sum(item => item.price * item.Quantity * 100) + (long)shippingPrice * 100,
                    Currency = "usd",
                    PaymentMethodTypes = new List<string> { "card" }
                };
                paymentIntent=await service.CreateAsync(options);
                basket.PaymentIntentId= paymentIntent.Id;
                basket.ClientSecret=paymentIntent.ClientSecret;
            }
            else
            {
                var options = new PaymentIntentUpdateOptions()
                {
                    Amount = (long)basket.Items.Sum(item => item.price * item.Quantity * 100) + (long)shippingPrice * 100
                };
                paymentIntent = await service.UpdateAsync(basket.PaymentIntentId,options);

            }
            await _basketRepository.UpdateBasketAsync(basket);
            return basket;
        }
    }
}
