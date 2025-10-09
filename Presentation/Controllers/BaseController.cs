using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseController:ControllerBase
    {
        protected string GetEmail() => User.FindFirst(ClaimTypes.Email)?.Value!;
        protected string GetId() => User.FindFirst(ClaimTypes.NameIdentifier)?.Value!;
    }
}
