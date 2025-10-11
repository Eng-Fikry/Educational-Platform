using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service_Abstraction;
using Shared;
using Shared.DTOs.Course;

namespace Presentation.Controllers
{
    public class CourseController(IServiceManger _serviceManger):BaseController
    {
        [HttpPost]
        [Authorize(Roles ="Teacher")]
        public async Task<ActionResult<CourseDto>> CreateCourse(CourseDto courseDto)
        {
            var Course = await _serviceManger.CourseService.CreateCourseAsync(courseDto);
            return Ok(Course);
        }
        [HttpDelete("{CourseId:guid}")]
        [Authorize(Roles = "Teacher")]

        public async Task<ActionResult<Message>> DeleteCourse(Guid CourseId)
        {
            var Course= await _serviceManger.CourseService.DeleteCourse(CourseId);
            return Ok(Course);
            
        }
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<CourseDto>>> GetAllCourses()
        {
            var Courses = await _serviceManger.CourseService.GetAllCourseAsync();
            return Ok(Courses);
        }
        [HttpGet("{CourseId:guid}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<CourseDto>>> GetCourse(Guid CourseId)
        {
            var Course = await _serviceManger.CourseService.GetCourseInformationsAsync(CourseId);
            return Ok(Course);
        }
        [HttpPut("{CourseId:guid}")]
        [Authorize]
        public async Task<ActionResult<CourseDto>> UpdateCourse(Guid CourseId,CourseDto courseDto)
        {
            var Course = await _serviceManger.CourseService.UpdateCourse(CourseId,courseDto);
            return Ok(Course);
        }
    }
}
