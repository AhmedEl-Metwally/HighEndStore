using AutoMapper;
using Domain.Contracts.BasketRepositorys;
using Domain.Contracts.UnitOfWorks;
using Domain.Entities.BasketModule;
using Domain.Entities.OrderModule;
using Domain.Exceptions;
using Microsoft.Extensions.Configuration;
using Services.Abstraction.Interface;
using Shared.DTOS.BasketsDto;
using Stripe;
using Product = Domain.Entities.ProductModule.Product;

namespace Services.Implementation
{
    public class PaymentService(IConfiguration _configuration,IBasketRepository _basketRepository,IUnitOfWork _unitOfWork,IMapper _mapper) : IPaymentService
    {
        public async Task<BasketDto> CreateOrUpdatePaymentIntentAsync(string basketId)
        {
            StripeConfiguration.ApiKey = _configuration.GetSection("StripeSettings")["SecretKey"];
            var basket = await GetBasketAsync(basketId);
            await ValidateBasketAsync(basket);
            var amount = CalculateTotalAsync(basket);
            await CreationOrUpdatePaymentIntentAsync(basket,amount);
            await _basketRepository.CreateOrUpdateBasketAsync(basket);
            return _mapper.Map<BasketDto>(basket);
        }


        //Helper Method

        private async Task<CustomerBasket> GetBasketAsync(string basketId)
            => await _basketRepository.GetBasketAsync(basketId) ?? throw new BasketNotFoundException(basketId);

        private async Task ValidateBasketAsync(CustomerBasket basket)
        {
            foreach (var item in basket.BasketItems)
            {
                var product = await _unitOfWork.GetRepository<Product, int>().GetByIdAsync(item.Id) ?? throw new ProductNotFoundException(item.Id);
                item.Price = product.Price;
            }

            if (!basket.DeliveryMethodId.HasValue)
                throw new Exception("Delivery method is not selected");
            var deliveryMethod = await _unitOfWork.GetRepository<DeliveryMethod, int>().GetByIdAsync(basket.DeliveryMethodId.Value)
                                                                     ?? throw new DeliveryMethodNotFoundException(basket.DeliveryMethodId.Value);
            basket.ShippingPrice = deliveryMethod.Price;

        }

        private long CalculateTotalAsync(CustomerBasket basket)
            => (long)(basket.BasketItems.Sum(I => I.Quantity * I.Price) + basket.ShippingPrice) * 100;

        private async Task CreationOrUpdatePaymentIntentAsync(CustomerBasket basket, long amount)
        {
            
            var stripeService = new PaymentIntentService();
            if (string.IsNullOrEmpty(basket.PaymentIntentId))
            {
                var options = new PaymentIntentCreateOptions()
                {
                    Amount = amount,
                    Currency = "usd",
                    PaymentMethodTypes = { "card" }
                };
                var paymentIntent = await stripeService.CreateAsync(options);
                basket.PaymentIntentId = paymentIntent.Id;
                basket.ClientSecret = paymentIntent.ClientSecret;
            }
            else
            {
                var options = new PaymentIntentUpdateOptions() { Amount = amount };
                await stripeService.UpdateAsync(basket.PaymentIntentId,options);
            }
        }
    }
}
