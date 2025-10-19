using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models.Courses;

namespace Service.Specifications
{
    public class GetCourseLessonsSpecification:BaseSpecification<Lesson,Guid>
    {
        public GetCourseLessonsSpecification(Guid CourseId):base(L=>L.CourseId==CourseId)
        {
            
        }
    }
}
