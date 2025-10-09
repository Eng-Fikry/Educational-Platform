using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Contracts;
using Domain.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Service.Specifications;
using Service_Abstraction;
using Shared.UserData;

namespace Service
{
    public class TeacherService(IUnitOfWork _unitOfWork, IMapper _mapper) : ITeacherService
    {
        public async Task<TeacherDto> GetTeacherData(string id)
        {
            var TeacherSpecification = new GetTeacherMainDataSpecification(id);
            var Teacher = await _unitOfWork.GetRepository<Teacher,int>().GetByIdAsync(TeacherSpecification);
            return _mapper.Map<TeacherDto>(Teacher);
        }
    }
}
