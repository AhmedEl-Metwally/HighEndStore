using Shared.DTOS.IdentityDto;

namespace Services.Abstraction.Interface
{
    public interface IAuthenticationService
    {
        Task<UserResultDto> LoginAsync(LoginDto loginDto);
        Task<UserResultDto> RegisterAsync(RegisterDto registerDto );
    }
}
