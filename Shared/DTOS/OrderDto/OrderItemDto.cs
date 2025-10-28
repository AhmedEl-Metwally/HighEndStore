namespace Shared.DTOS.OrderDto
{
    public record OrderItemDto
    {
        public int ProductId { get; init; }
        public string ProductName { get; init; } = string.Empty;
        public string ProductUrl { get; init; } = string.Empty;
        public decimal Price { get; init; }
        public int Quantity { get; init; }

    }
}