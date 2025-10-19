using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared;
using Shared.DTOs.Course;

namespace Service_Abstraction
{
    public interface ILessonService
    {
        Task<CourseLessonsDto> AddLessonsAsync(CourseLessonsDto courseLessons, string Email);
        Task<CourseLessonsDto> GetLessonsAsync(Guid CourseId);
        Task<LessonDto> UpdateLessonAsync(Guid LessonId ,LessonDto lesson, string Email);
        Task<Message> DeleteLessonAsync(Guid LessonId, string Email);

    }
}
