using Shared.DTOS.BasketsDto;

namespace Services.Abstraction.Interface
{
    public interface IPaymentService
    {
        Task<BasketDto> CreateOrUpdatePaymentIntentAsync(string basketId);
    }
}
