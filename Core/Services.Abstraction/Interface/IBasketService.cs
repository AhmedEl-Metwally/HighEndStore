using Shared.DTOS.BasketsDto;

namespace Services.Abstraction.Interface
{
    public interface IBasketService
    {
        Task<BasketDto> GetBasketAsync(string id);
        Task<bool> DeleteBasketAsync(string id);
        Task<BasketDto> CreateOrUpdateBasketAsync(BasketDto basketDto);
    }
}
