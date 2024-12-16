using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Order_Aggrate;

namespace Talabat.Core.Specifications
{
    public class OrderSpecification : BaseSpecification<Order>
    {
        public OrderSpecification(string buyerEmail) :base(O=>O.BayerEmail == buyerEmail)
        {
            Inclides.Add(O => O.DeliveryMethod);
            Inclides.Add(O => O.OrderItems);
            AddOrderByDesc(O => O.OrderDate);

        }
        public OrderSpecification(int id,string buyerEmail) : base(O => O.BayerEmail == buyerEmail&&O.Id==id)
        {
            Inclides.Add(O => O.DeliveryMethod);
            Inclides.Add(O => O.OrderItems);  
        }
    }
}
