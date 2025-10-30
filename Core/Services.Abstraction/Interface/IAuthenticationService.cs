using Shared.DTOS.IdentityDto;
using Shared.DTOS.OrderDto;

namespace Services.Abstraction.Interface
{
    public interface IAuthenticationService
    {
        Task<UserResultDto> LoginAsync(LoginDto loginDto);
        Task<UserResultDto> RegisterAsync(RegisterDto registerDto );


        Task<UserResultDto> GetCurrentUserAsync(string userEmail);
        Task<bool> CheckEmailExistAsync(string userEmail);
        Task<AddressDto> GetUserAddressAsync(string userEmail);
        Task<AddressDto> UpdateUserAddressAsync(string userEmail,AddressDto addressDto );
    }
}
