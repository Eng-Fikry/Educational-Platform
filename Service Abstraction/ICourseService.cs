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
        Task<IEnumerable<CourseDto>> GetAllCourseAsync(int TeacherId);
        Task<CourseDto> GetCourseInformationsAsync(Guid CourseId);
        Task<Message> DeleteCourse(Guid CourseId);
        Task<CourseDto> UpdateCourse(Guid CourseId,CourseDto CourseDto);

    }
}
