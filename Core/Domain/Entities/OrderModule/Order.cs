using ShippingAddress = Domain.Entities.OrderModule.Address;

namespace Domain.Entities.OrderModule
{
    public class Order : BaseEntity<Guid>
    {
        public string UserEmail { get; set; } = string.Empty;
        public Address ShippingAddress  { get; set; } = new Address();
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        public OrderPaymentStatus OrderPaymentStatus { get; set; } = OrderPaymentStatus.Pending;
        public DeliveryMethod DeliveryMethod { get; set; } = default!;
        public int? DeliveryMethodId { get; set; }
        public decimal SubTotal { get; set; }
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.UtcNow;
        public string PaymentIntentId { get; set; } = string.Empty;
    }
}
