using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Contracts;
using Domain.Models.Identity;
using Service.Specifications;
using Service_Abstraction;
using Shared.DTOs.UserData;

namespace Service
{
    public class StudentService(IUnitOfWork _unitOfWork,IMapper _mapper) : IStudentService
    {
        public async Task<StudentDto> GetStudentData(string id)
        {
            var StudentSpecification = new GetStudentMainDataSpecification(id);
            var Student = await _unitOfWork.GetRepository<Student, int>().GetByIdAsync(StudentSpecification);
            return _mapper.Map<StudentDto>(Student);
        }
    }
}
