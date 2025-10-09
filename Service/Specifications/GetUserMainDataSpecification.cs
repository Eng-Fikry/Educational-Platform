using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Domain.Models.Identity;

namespace Service.Specifications
{
    public class GetTeacherMainDataSpecification : BaseSpecification<Teacher, int>
    {
        public GetTeacherMainDataSpecification(string id) : base(S => S.UserId == id)
        {
        }
    }
    public class GetStudentMainDataSpecification : BaseSpecification<Student, int>
    {
        public GetStudentMainDataSpecification(string id) : base(S => S.UserId==id)
        {
        }
    }
}
