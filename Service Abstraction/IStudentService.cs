using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.UserData;

namespace Service_Abstraction
{
    public interface IStudentService
    {
        Task<StudentDto> GetStudentData(string id);
    }
}
