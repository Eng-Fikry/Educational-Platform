using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service_Abstraction;
using Shared.DTOs.UserData;

namespace Presentation.Controllers
{
    public class TeacherController(IServiceManger _serviceManger):BaseController
    {
        [Authorize(Roles ="Teacher")]
        [HttpGet]
        public async Task<ActionResult<TeacherDto>> GetTeacherData()
        {
            var Teacher =await _serviceManger.TeacherService.GetTeacherData(GetId());
            return Ok(Teacher);

        }

    }
}
