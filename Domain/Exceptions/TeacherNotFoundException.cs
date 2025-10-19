using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class TeacherNotFoundException(int id):NotFoundException($"Teacher with id: {id} Not Found")
    {
    }
}
