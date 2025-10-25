using Microsoft.AspNetCore.Mvc;
using Services.Abstraction.Interface;
using Shared.DTOS.BasketsDto;

namespace HighEndStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController(IServiceManager _serviceManager) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<BasketDto>> GetBasketAsync(string id)
            => Ok(await _serviceManager.BasketService.GetBasketAsync(id));

        [HttpPost]
        public async Task<ActionResult<BasketDto>> CreateOrUpdateBasketAsync(BasketDto basket)
            => Ok(await _serviceManager.BasketService.CreateOrUpdateBasketAsync(basket));

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBasketAsync(string id)
        {
           await _serviceManager.BasketService.DeleteBasketAsync(id);
           return NoContent();
        }
    }
}
