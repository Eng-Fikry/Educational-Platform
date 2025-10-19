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
    public class LessonController(IServiceManger _serviceManger):BaseController
    {
        [HttpPost]
        [Authorize(Roles ="Teacher")]
        public async Task<ActionResult<CourseLessonsDto>> CreateLessonsForCourse(CourseLessonsDto courseLessons)
        {
            var lessons= await _serviceManger.LessonService.AddLessonsAsync(courseLessons,GetEmail());
            return Ok(lessons);
        }


        [HttpGet("{CourseId:guid}")]
        [Authorize]
        public async Task<ActionResult<CourseLessonsDto>> GetCourseLessons(Guid CourseId)
        {
            var lessons = await _serviceManger.LessonService.GetLessonsAsync(CourseId);
            return Ok(lessons);
        }


        [HttpDelete("{LessonId:guid}")]
        [Authorize(Roles ="Teacher")]
        public async Task<ActionResult<Message>> DeleteLesson(Guid LessonId)
        {
            var message = await _serviceManger.LessonService.DeleteLessonAsync(LessonId, GetEmail());
            return Ok(message);
        }


        [HttpPut("{LessonId:guid}")]
        [Authorize(Roles = "Teacher")]
        public async Task<ActionResult<Message>> UpdateLesson(Guid LessonId,LessonDto lessonDto)
        {
            var Lesson = await _serviceManger.LessonService.UpdateLessonAsync(LessonId, lessonDto, GetEmail());
            return Ok(Lesson);
        }
    }
}
