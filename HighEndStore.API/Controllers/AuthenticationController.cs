using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstraction.Interface;
using Shared.DTOS.IdentityDto;
using Shared.DTOS.OrderDto;
using System.Security.Claims;

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


        [HttpGet("EmailExist")]
        public async Task<ActionResult<bool>> CheckEmailExistAsync(string email)
            => Ok(await _serviceManager.AuthenticationService.CheckEmailExistAsync(email));

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<UserResultDto>> GetCurrentUserAsync()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await _serviceManager.AuthenticationService.GetCurrentUserAsync(email);
            return Ok(user);
        }

        [Authorize]
        [HttpGet("Address")]
        public async Task<ActionResult<AddressDto>> GetUserAddressAsync()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var address = await _serviceManager.AuthenticationService.GetUserAddressAsync(email);
            return Ok(address);
        }

        [Authorize]
        [HttpPut("Address")]
        public async Task<ActionResult<AddressDto>> UpdateUserAddressAsync(AddressDto addressDto)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var address = await _serviceManager.AuthenticationService.UpdateUserAddressAsync(email, addressDto);
            return Ok(address);
        }


    }
}
