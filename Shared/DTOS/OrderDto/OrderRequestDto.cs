namespace Shared.DTOS.OrderDto
{
    public record OrderRequestDto
    {
        public string BasketId { get; init; } = string.Empty;
        public AddressDto ShippingAddress { get; init; } = default!;
        public int DeliveryMethodId { get; init; }
    }
}
