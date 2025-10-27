using Domain.Entities.IdentityModule;
using Domain.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Services.Abstraction.Interface;
using Shared.Common;
using Shared.DTOS.IdentityModule;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Services.Implementation
{
    public class AuthenticationService(UserManager<User> _userManager,IOptions<JwtOption> _options) : IAuthenticationService
    {
        public async Task<UserResultDto> LoginAsync(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
                if(user is null) throw new UnAuthenticationException();
           
            var result = await _userManager.CheckPasswordAsync(user, loginDto.Password);
            if (!result)
                throw new UnAuthenticationException();
            return new UserResultDto( user.DisplayName, await CreateTokenAsync(user),user.Email);
        }

        public async Task<UserResultDto> RegisterAsync(RegisterDto registerDto)
        {
            var user = new User
            {
                DisplayName = registerDto.DisplayName,
                Email = registerDto.Email,
                UserName = registerDto.Username,
                PhoneNumber = registerDto.PhoneNumber
            };

            var result = await _userManager.CreateAsync(user,registerDto.Password );
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(E => E.Description).ToList();
                // throw new ValidationException(errors);
                foreach (var error in errors)
                {
                    Console.WriteLine(error);
                }

                throw new ValidationException(errors);
            }

            return new UserResultDto(user.DisplayName, await CreateTokenAsync(user), user.Email);
        }


        private async Task<string> CreateTokenAsync(User user)
        {
            var JwtOptions = _options.Value;

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.DisplayName),
                new Claim(ClaimTypes.Email, user.Email)
            };

            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
                claims.Add(new Claim(ClaimTypes.Role,role));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtOptions.SecretKey));
            var signInCreds = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken
                (
                    issuer : JwtOptions.Issuer,
                    audience : JwtOptions.Audience,
                    claims : claims,
                    expires : DateTime.UtcNow.AddDays(JwtOptions.ExpirationInDays),
                    signingCredentials : signInCreds
                );

            return new JwtSecurityTokenHandler().WriteToken(token);


        }


    }
}
