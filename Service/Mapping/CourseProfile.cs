using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Models.Courses;
using Shared.DTOs.Course;

namespace Service.Mapping
{
    public class CourseProfile:Profile
    {
        public CourseProfile()
        {
            CreateMap<CourseDto,Course>().ReverseMap();
        }
    }
}
