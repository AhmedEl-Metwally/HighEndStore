
namespace Shared.DTOS.BasketsDto
{
    public record BasketDto
    {
        public string Id { get; init; } = string.Empty;
        public ICollection<BasketItemDto> BasketItems { get; init; } = new List<BasketItemDto>();

        public string PaymentIntentId { get; init; } = string.Empty;
        public string ClientSecret { get; init; } = string.Empty;
        public decimal? ShippingPrice { get; init; }
        public int? DeliveryMethodId { get; init; }

    }
}
