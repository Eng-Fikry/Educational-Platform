using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Domain.Models.Identity
{
    public class User:IdentityUser
    {
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Country { get; set; } = default!;
        public string DisplayName => $"{FirstName} {LastName}";

        


    }
}
