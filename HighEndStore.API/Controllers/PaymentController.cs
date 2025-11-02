using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Abstraction.Interface;
using Shared.DTOS.BasketsDto;

namespace HighEndStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController(IServiceManager _serviceManager) : ControllerBase
    {
        [HttpPost("{basketId}")]
        public async Task<ActionResult<BasketDto>> CreateOrUpdatePaymentIntentAsync(string basketId)
            => Ok(await _serviceManager.PaymentService.CreateOrUpdatePaymentIntentAsync(basketId));
    }
}
