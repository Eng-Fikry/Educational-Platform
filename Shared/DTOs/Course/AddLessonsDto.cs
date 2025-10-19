using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs.Course
{
    public class CourseLessonsDto
    {
        public required Guid CourseId { get; set; }
        public IEnumerable<LessonDto> Lessons { get; set; } = new List<LessonDto>();
    }
}
