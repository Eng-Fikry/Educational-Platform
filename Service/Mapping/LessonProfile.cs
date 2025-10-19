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
    public class LessonProfile:Profile
    {
        public LessonProfile()
        {
            CreateMap<Lesson,LessonDto>().ReverseMap();
            CreateMap<Lesson, LessonDto>().ForMember(dist => dist.Thumbnail, options => options.MapFrom<LessonPictureResolver>())
                    .ForMember(dist => dist.VideoUrl, options => options.MapFrom<LessonVideoResolver>()).ReverseMap();

        }
    }
}
