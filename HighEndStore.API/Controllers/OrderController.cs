using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstraction.Interface;
using Shared.DTOS.OrderDto;
using System.Security.Claims;

namespace HighEndStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrderController(IServiceManager _serviceManager) : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<OrderResultDto>> CreateOrderAsync(OrderRequestDto orderRequest )
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            var order = await _serviceManager.OrderService.CreateOrderAsync(orderRequest, userEmail);
            return Ok(order);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<OrderResultDto>> GetOrderByIdAsync(Guid id)
        {
            var order = await _serviceManager.OrderService.GetOrderByIdAsync(id);
            return Ok(order);   
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderResultDto>>> GetAllOrderByEmailAsync()
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            var orders = await _serviceManager.OrderService.GetOrderByEmailAsync(userEmail);
            return Ok(orders);
        }

        [HttpGet("DeliveryMethod")]
        public async Task<ActionResult<IEnumerable<DeliveryMethodResultDto>>> GetDeliveryMethodAsync()
        {
            var deliveryMethod =await _serviceManager.OrderService.GetDeliveryMethodsAsync();
            return Ok(deliveryMethod);
        }
    }
}
