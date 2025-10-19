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
    public class LessonPictureResolver(IConfiguration _configuration) : IValueResolver<Lesson, LessonDto, string>
    {
        public string Resolve(Lesson source, LessonDto destination, string destMember, ResolutionContext context)
        {
            if(string.IsNullOrEmpty(source.Thumbnail))
                return string.Empty;
            var BaseUrl = _configuration["BaseUrl:URL"];
            return $"{BaseUrl}{source.Thumbnail}";
        }
    }

    public class LessonVideoResolver(IConfiguration _configuration) : IValueResolver<Lesson, LessonDto, string>
    {
        
        public string Resolve(Lesson source, LessonDto destination, string destMember, ResolutionContext context)
        {
            if (string.IsNullOrEmpty(source.VideoUrl))
                return string.Empty;
            var BaseUrl = _configuration["BaseUrl:URL"];
            return $"{BaseUrl}{source.VideoUrl}";
        }
    }
    
}
