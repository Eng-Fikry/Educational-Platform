using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service_Abstraction
{
    public interface IServiceManger
    {
        public IAuthnticationService AuthnticationService { get; }
        public ITeacherService TeacherService { get; }
        public IStudentService StudentService { get; }
        public ICourseService CourseService { get; }

    }
}
