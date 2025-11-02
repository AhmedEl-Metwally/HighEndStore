using AutoMapper;
using Domain.Contracts.BasketRepositorys;
using Domain.Contracts.UnitOfWorks;
using Domain.Entities.BasketModule;
using Domain.Entities.OrderModule;
using Domain.Entities.ProductModule;
using Domain.Exceptions;
using Services.Abstraction.Interface;
using Services.Specifications;
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

            var orderRepo = _unitOfWork.GetRepository<Order,Guid>();

            var deliveryMethod = await _unitOfWork.GetRepository<DeliveryMethod,int>().GetByIdAsync(order.DeliveryMethodId) 
                                                            ?? throw new DeliveryMethodNotFoundException(order.DeliveryMethodId);

            var orderExists = await orderRepo.GetByIdAsync(new OrderWithPaymentIntentIdSpecifications(basket.PaymentIntentId));
            if (orderExists != null)
                orderRepo.Delete(orderExists);

            var subTotal = orderItem.Sum(O => O.Price * O.Quantity);
            var orderToCreate = new Order(userEmail,address,orderItem,deliveryMethod,subTotal,basket.PaymentIntentId);
            await orderRepo.AddAsync(orderToCreate);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<OrderResultDto>(orderToCreate);
        }

        public async Task<IEnumerable<DeliveryMethodResultDto>> GetDeliveryMethodsAsync()
        {
            var deliveryMethodResult = await _unitOfWork.GetRepository<DeliveryMethod,int>().GetAllAsync();
            return _mapper.Map<IEnumerable<DeliveryMethodResultDto>>(deliveryMethodResult);
        }

        public async Task<OrderResultDto> GetOrderByIdAsync(Guid id)
        {
            var order = await _unitOfWork.GetRepository<Order,Guid>().GetByIdAsync(new OrderWithIncludesSpecifications(id)) ?? throw new OrderNotFoundException(id);
            return _mapper.Map<OrderResultDto>(order);
        }

        public async Task<IEnumerable<OrderResultDto>> GetOrderByEmailAsync(string userEmail)
        {
            var orders = await _unitOfWork.GetRepository<Order, Guid>().GetAllAsync(new OrderWithIncludesSpecifications(userEmail));
            return _mapper.Map<IEnumerable<OrderResultDto>>(orders);
        }


        // HelpersMethods
        private OrderItem CreateOrderItem(Product product, BasketItem baskets)
        {
            var ProductInOrderItem = new ProductInOrderItem(product.Id, product.Name, product.PictureUrl);
            return new OrderItem(ProductInOrderItem, product.Price, baskets.Quantity);
        }
    }
}
