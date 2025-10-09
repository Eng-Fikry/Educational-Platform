using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Identity
{
    public class Student:Base<int>
    {
        public string UserName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;
        //public string DisplayName { get; set; } = default!;
        public DateTime JoinedDate { get; set; } = DateTime.Today;
        public string Role { get; set; } = default!;
        public string UserId { get; set; } = default!;
    }
}
