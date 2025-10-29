using AutoMapper;
using Domain.Contracts.BasketRepositorys;
using Domain.Contracts.UnitOfWorks;
using Domain.Entities.BasketModule;
using Domain.Entities.OrderModule;
using Domain.Entities.ProductModule;
using Domain.Exceptions;
using Services.Abstraction.Interface;
using Shared.DTOS.OrderDto;

namespace Services.Implementation
{
    public class OrderService(IMapper _mapper,IBasketRepository _basketRepository,IUnitOfWork _unitOfWork) : IOrderService
    {
        public async Task<OrderResultDto> CreateOrderAsync(OrderRequestDto order, string userEmail)
        {
            var address = _mapper.Map<Address>(order.ShippingAddress);
            var basket = await _basketRepository.GetBasketAsync(order.BasketId) ?? throw new BasketNotFoundException(order.BasketId);
            var orderItem = new List<OrderItem>();

            foreach (var baskets in basket.BasketItems)
            {
               var product = await _unitOfWork.GetRepository<Product, int>().GetByIdAsync(baskets.Id) ?? throw new ProductNotFoundException(baskets.Id);
                orderItem.Add(CreateOrderItem(product,baskets));
            }

            var deliveryMethod = await _unitOfWork.GetRepository<DeliveryMethod,int>().GetByIdAsync(order.DeliveryMethodId) 
                                                            ?? throw new DeliveryMethodNotFoundException(order.DeliveryMethodId);
            var subTotal = orderItem.Sum(O => O.Price * O.Quantity);
            var orderToCreate = new Order(userEmail,address,orderItem,deliveryMethod,subTotal);
            await _unitOfWork.GetRepository<Order, Guid>().AddAsync(orderToCreate);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<OrderResultDto>(orderToCreate);
        }

        public Task<IEnumerable<DeliveryMethodResultDto>> GetDeliveryMethodsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<OrderResultDto>> GetOrderByEmailAsync(string userEmail)
        {
            throw new NotImplementedException();
        }

        public Task<OrderResultDto> GetOrderByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }


        // HelpersMethods
        private OrderItem CreateOrderItem(Product product, BasketItem baskets)
        {
            var ProductInOrderItem = new ProductInOrderItem(product.Id, product.Name, product.PictureUrl);
            return new OrderItem(ProductInOrderItem, product.Price, baskets.Quantity);
        }


    }
}
