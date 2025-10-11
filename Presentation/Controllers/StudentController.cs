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
    public class StudentController(IServiceManger _serviceManger) : BaseController
    {
        [Authorize(Roles = "Student")]
        [HttpGet]
        public async Task<ActionResult<StudentDto>> GetTeacherData()
        {
            var Student = await _serviceManger.StudentService.GetStudentData(GetId());
            return Ok(Student);

        }
    }
}
