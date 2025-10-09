using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Models.Identity;
using Shared.UserData;

namespace Service.Mapping
{
    public class TeacherProfile:Profile
    {
        public TeacherProfile()
        {
            CreateMap<Teacher,TeacherDto>().ReverseMap();
        }
    }
}
