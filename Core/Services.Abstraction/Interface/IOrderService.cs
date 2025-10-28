using Shared.DTOS.OrderDto;

namespace Services.Abstraction.Interface
{
    public interface IOrderService
    {
        Task<OrderResultDto> GetOrderByIdAsync(Guid id);
        Task<IEnumerable<OrderResultDto>> GetOrderByEmailAsync(string userEmail);
        Task<OrderResultDto> CreateOrderAsync(OrderRequestDto order, string userEmail);   
        Task<IEnumerable<DeliveryMethodResultDto>> GetDeliveryMethodsAsync();
    }
}
