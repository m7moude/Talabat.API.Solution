using Stripe;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entities.Order_Aggrate
{
    public class Order : BaseEntity
    {
        public Order()
        {
            
        }
        public Order(string bayerEmail, Address shippigAddress, DeliveryMethod deliveryMethod, decimal subTotal,ICollection<OrderItem> items, string paymentIntentId)
        {
            BayerEmail = bayerEmail;
            ShippigAddress = shippigAddress;
            DeliveryMethod = deliveryMethod;
            SubTotal = subTotal;
            OrderItems = items;
            PaymentIntentId = paymentIntentId;
        }

        public string BayerEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public Address ShippigAddress { get; set; }
        public DeliveryMethod DeliveryMethod { get; set; } //Nav[One]

        //public int DeliveryMethodId { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }= new HashSet<OrderItem>(); // Nav [Many]
        public decimal SubTotal { get; set; }
        //[NotMapped]
        //public decimal Total => SubTotal + DeliveryMethod.Cost;
        public string PaymentIntentId { get; set; } 

        public decimal Total()
            => SubTotal + DeliveryMethod.Cost;

    }
}
