
namespace Shared.DTOS.BasketsDto
{
    public record BasketDto
    {
        public string Id { get; init; } = string.Empty;
        public ICollection<BasketItemDto> BasketItems { get; init; } = new List<BasketItemDto>();
    }
}
