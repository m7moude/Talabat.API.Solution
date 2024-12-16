using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entities.Order_Aggrate
{
    public class OrderItem:BaseEntity
    {
        public OrderItem()
        {
            
        }
        public OrderItem(ProductOrderItem product, decimal price, int quntity)
        {
            Product = product;
            Price = price;
            Quntity = quntity;
        }

        public ProductOrderItem Product { get; set; }

        public decimal Price { get; set; }
        public int Quntity { get; set; }

    }
}
