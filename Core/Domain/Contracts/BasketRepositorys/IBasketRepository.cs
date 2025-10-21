
using Domain.Entities.BasketModule;

namespace Domain.Contracts.BasketRepositorys
{
    public interface IBasketRepository
    {
        Task<CustomerBasket?> GetBasketAsync(string id);
        Task<CustomerBasket?> CreateOrUpdateBasketAsync(CustomerBasket basket,TimeSpan? timeToLive = null);
        Task<bool> DeleteBasketAsync(string id);
    }
}
