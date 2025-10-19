using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared;
using Shared.DTOs.Course;

namespace Service_Abstraction
{
    public interface ICourseService
    {
        Task<CourseDto> CreateCourseAsync(CourseDto CourseDto);
        Task<IEnumerable<CourseDto>> GetAllCourseAsync();
        Task<IEnumerable<CourseDto>> GetTeacherCoursesAsync(int TeacherId);
        Task<CourseDto> GetCourseInformationsAsync(Guid CourseId);
        Task<Message> DeleteCourse(Guid CourseId, string Email);
        Task<CourseDto> UpdateCourse(Guid CourseId,CourseDto CourseDto, string Email);

    }
}
