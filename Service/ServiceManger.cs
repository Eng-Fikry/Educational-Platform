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
using Service_Abstraction;

namespace Service
{
    public class ServiceManger(IConfiguration configuration,IUnitOfWork unitOfWork,UserManager<User> userManager, IMapper mapper) : IServiceManger
    {
        private readonly Lazy<IAuthnticationService> _authntication=new Lazy<IAuthnticationService>(()=>new AuthnticationService(configuration, userManager,unitOfWork,mapper));
        public IAuthnticationService AuthnticationService => _authntication.Value;


        private readonly Lazy<ITeacherService> teacher = new Lazy<ITeacherService>(() => new TeacherService(unitOfWork, mapper));
        public ITeacherService TeacherService => teacher.Value;

        private readonly Lazy<IStudentService> student = new Lazy<IStudentService>(() => new StudentService(unitOfWork, mapper));
        public IStudentService StudentService => student.Value;

        private readonly Lazy<ICourseService> course = new Lazy<ICourseService>(() => new CourseService(unitOfWork, mapper));
        public ICourseService CourseService => course.Value;
    }
}
