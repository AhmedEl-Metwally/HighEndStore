namespace Shared.DTOS.BasketsDto
{
    public record BasketItemDto
    {
        public int Id { get; init; }
        public string ProductName { get; init; } = string.Empty;
        public decimal Price { get; init; }
        public string PictureUrl { get; init; } = string.Empty;
        public int Quantity { get; init; }
    }
}