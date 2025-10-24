using AutoMapper;
using Domain.Contracts.BasketRepositorys;
using Domain.Entities.BasketModule;
using Domain.Exceptions;
using Services.Abstraction.Interface;
using Shared.DTOS.BasketsDto;

namespace Services.Implementation
{
    public class BasketService(IBasketRepository _basketRepository , IMapper _mapper) : IBasketService
    {
        public async Task<BasketDto> CreateOrUpdateBasketAsync(BasketDto basketDto)
        {
            var basket = _mapper.Map<CustomerBasket>(basketDto);
            var createOrUpdateBasket = await _basketRepository.CreateOrUpdateBasketAsync(basket);
            return createOrUpdateBasket is null ? throw new Exception("Failed to create or update basket") : _mapper.Map<BasketDto>(createOrUpdateBasket);
        }

        public async Task<bool> DeleteBasketAsync(string id) => await _basketRepository.DeleteBasketAsync(id);

        public async Task<BasketDto> GetBasketAsync(string id)
        {
            var basket = await _basketRepository.GetBasketAsync(id);
            return basket is null ? throw new BasketNotFoundException(id) : _mapper.Map<BasketDto>(basket);
        }
    }
}
