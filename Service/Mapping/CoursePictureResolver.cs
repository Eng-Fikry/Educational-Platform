using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Models.Courses;
using Microsoft.Extensions.Configuration;
using Shared.DTOs.Course;

namespace Service.Mapping
{
    public class CoursePictureResolver(IConfiguration _configuration) : IValueResolver<Course, CourseDto, string>
    {
        public string Resolve(Course source, CourseDto destination, string destMember, ResolutionContext context)
        {
            if (string.IsNullOrEmpty(source.Thumbnail))
                return string.Empty;
            var BaseUrl = _configuration["BaseUrl:URL"];
            return $"{BaseUrl}{source.Thumbnail}";

        }
    }
}
