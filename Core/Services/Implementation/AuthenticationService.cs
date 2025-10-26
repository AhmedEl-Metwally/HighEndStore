﻿using Domain.Entities.IdentityModule;
using Domain.Exceptions;
using Microsoft.AspNetCore.Identity;
using Services.Abstraction.Interface;
using Shared.DTOS.IdentityModule;

namespace Services.Implementation
{
    public class AuthenticationService(UserManager<User> _userManager) : IAuthenticationService
    {
        public async Task<UserResultDto> LoginAsync(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
                if(user is null) throw new UnAuthenticationException();
           
            var result = await _userManager.CheckPasswordAsync(user, loginDto.Password);
            if (!result)
                throw new UnAuthenticationException();
            return new UserResultDto( user.DisplayName, "Token",user.Email);
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
                throw new ValidationException(errors);
            }

            return new UserResultDto(user.DisplayName, "Token", user.Email);
        }
    }
}
