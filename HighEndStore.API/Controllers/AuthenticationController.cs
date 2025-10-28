using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Abstraction.Interface;
using Shared.DTOS.IdentityDto;

namespace HighEndStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController(IServiceManager _serviceManager) : ControllerBase
    {
        [HttpPost("Register")]
        public async Task<ActionResult<UserResultDto>> RegisterAsync([FromBody]RegisterDto registerDto) 
            => Ok(await _serviceManager.AuthenticationService.RegisterAsync(registerDto));

        [HttpPost("Login")]
        public async Task<ActionResult<UserResultDto>> LoginAsync([FromBody]LoginDto loginDto) 
            => Ok(await _serviceManager.AuthenticationService.LoginAsync(loginDto));
    }
}
