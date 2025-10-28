namespace Domain.Entities.OrderModule
{
    public class OrderItem : BaseEntity<Guid>
    {
        public ProductInOrderItem ProductInOrderItem { get; set; } = default!;
        public decimal Price { get; set; }
        public int Quantity { get; set; }

    }
}