using Domain.Entities.OrderModule;

namespace Services.Specifications
{
    public class OrderWithPaymentIntentIdSpecifications : BaseSpecifications<Order,Guid>
    {
        public OrderWithPaymentIntentIdSpecifications(string PaymentIntentId) :base(P => P.PaymentIntentId == PaymentIntentId)
        {
            
        }
    }
}
