using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Contracts;
using Domain.Models.Courses;
using Service_Abstraction;
using Shared;
using Shared.DTOs.Course;

namespace Service
{
    public class CourseService(IUnitOfWork _unitOfWork , IMapper _mapper) : ICourseService
    {
        public async Task<CourseDto> CreateCourseAsync(CourseDto CourseDto)
        {
            var CourseRepo = _unitOfWork.GetRepository<Course, Guid>();
            var Course =  _mapper.Map<Course>(CourseDto);
            await CourseRepo.AddAsync(Course);
            await _unitOfWork.SaveAsync();
            return CourseDto;
        }

        public async Task<Message> DeleteCourse(Guid CourseId) 
        {
            var Course = await _unitOfWork.GetRepository<Course, Guid>().GetByIdAsync(CourseId);
            _unitOfWork.GetRepository<Course, Guid>().Remove(_mapper.Map<Course>(Course));
            await _unitOfWork.SaveAsync();
            return new Message() { message = "Course Deleted Successfuly" };
        } 
        //Remove(_mapper.Map<Course>(CourseDto));

        public async Task<IEnumerable<CourseDto>> GetAllCourseAsync()
        {
           var Courses= await _unitOfWork.GetRepository<Course,Guid>().GetAllAsync();
           var CoursesDto = _mapper.Map<IEnumerable<CourseDto>>(Courses); 
           return CoursesDto;
        }

        public async Task<IEnumerable<CourseDto>> GetAllCourseAsync(int TeacherId)
        {
            var Courses = await _unitOfWork.GetRepository<Course, Guid>().GetAllAsync();
            var TeacherCourses = Courses.Where(T => T.TeacherId == TeacherId);
            var TeacherCoursesDto = _mapper.Map<IEnumerable<CourseDto>>(TeacherCourses);
            return TeacherCoursesDto;
        }

        

        public async Task<CourseDto> GetCourseInformationsAsync(Guid CourseId)
        {
            var Course = await _unitOfWork.GetRepository<Course, Guid>().GetByIdAsync(CourseId);
            var CourseDto = _mapper.Map<CourseDto>(Course);
            return CourseDto;

        }

        public async Task<CourseDto> UpdateCourse(Guid CourseId, CourseDto CourseDto)
        {
            var course = _mapper.Map<Course>(CourseDto);
            course.Id = CourseId;
            course.Updated= DateTime.UtcNow.Date;
            _unitOfWork.GetRepository<Course, Guid>().Update(course);
            await _unitOfWork.SaveAsync();
            return _mapper.Map<CourseDto>(course);

        }


    }
}
