using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Identity
{
    public class UserDetailsDto
    {
        public int Id { get; set; }
        public string UserName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Country { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;
        public string DisplayName { get; set; } = default!;
    }
}
