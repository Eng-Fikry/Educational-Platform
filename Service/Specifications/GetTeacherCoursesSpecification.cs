using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models.Courses;

namespace Service.Specifications
{
    public class GetTeacherCoursesSpecification:BaseSpecification<Course,Guid>
    {
        public GetTeacherCoursesSpecification(int TeacherId):base(C=>C.TeacherId==TeacherId)
        {
            
        }
    }
}
