using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs.Identity
{
    public record RegisterDto
    {
        public required string FirstName { get; set; } = default!;
        public required string LastName { get; set; } = default!;
        public string DisplayName => $"{FirstName} {LastName}";
        public string UserName => Email.Split('@')[0];
        [EmailAddress]
        public required string Email { get; set; } = default!;
        public required string Country { get; set; } = default!;
        [Phone]
        public required string PhoneNumber { get; set; } = default!;
        public required string Role { get; set; } = default!;
        public required string Password { get; set; } = default!;
        public required string ConfirmPassword { get; set; } = default!;

    }
}
