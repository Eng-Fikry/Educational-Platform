using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Identity;

namespace Service_Abstraction
{
    public interface IAuthnticationService
    {
        Task<UserDto> RegisterAsync(RegisterDto Data);
        Task<UserDto> LoginAsync(LoginDto Data);
        Task<bool> CheckEmailExistAsync(string Email);
        Task<UserDto> GetUserAsync(string Email);


    }
}
