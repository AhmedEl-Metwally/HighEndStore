
namespace Domain.Entities.OrderModule
{
    public class ProductInOrderItem
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string ProductUrl { get; set; } = string.Empty;
    }
}
