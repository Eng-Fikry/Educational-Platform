using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service_Abstraction;
using Shared.DTOs.Identity;

namespace Presentation.Controllers
{
    
    public class AuthnticationController(IServiceManger _serviceManger) : BaseController
    {
        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto register)
        {
            var signup = await _serviceManger.AuthnticationService.RegisterAsync(register);
            return Ok(signup);
        }
        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto login)
        {
            var signin = await _serviceManger.AuthnticationService.LoginAsync(login);
            return Ok(signin);
        }
        [Authorize]
        [HttpGet()]
        public async Task<ActionResult<UserDto>> GetUserData()
        {
            var UserData = await _serviceManger.AuthnticationService.GetUserAsync(GetEmail());
            return Ok(UserData);
        }
        [HttpGet("CheckEmailExist")]
        public async Task<ActionResult<bool>> CheckEmailExis(string Email)
        {
            var Check = await _serviceManger.AuthnticationService.CheckEmailExistAsync(Email);
            return Ok(Check);
        }


    }
}
