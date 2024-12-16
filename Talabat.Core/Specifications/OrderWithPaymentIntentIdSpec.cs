using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Order_Aggrate;

namespace Talabat.Core.Specifications
{
    public class OrderWithPaymentIntentIdSpec : BaseSpecification<Order>
    {
        public OrderWithPaymentIntentIdSpec(string paymentIntent):base(O=>O.PaymentIntentId==paymentIntent)
        {
            
        }
    }
}
