﻿using System.ComponentModel.DataAnnotations;

namespace Shared.DTOS.IdentityDto
{
    public record RegisterDto
    {
        public string DisplayName { get; init; } = string.Empty;
        [EmailAddress]
        public string Email { get; init; } = string.Empty;
        public string Password { get; init; } = string.Empty;
        public string Username { get; init; } = string.Empty;
        [Phone]
        public string? PhoneNumber { get; init; }
    }
}
