using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Contracts;
using Domain.Exceptions;
using Domain.Models.Courses;
using Domain.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Service.Specifications;
using Service_Abstraction;
using Shared;
using Shared.DTOs.Course;

namespace Service
{
    public class CourseService(IUnitOfWork _unitOfWork , IMapper _mapper,UserManager<User> _userManager) : ICourseService
    {
        public async Task<CourseDto> CreateCourseAsync(CourseDto CourseDto)
        {
            var CourseRepo = _unitOfWork.GetRepository<Course, Guid>();
            var Course =  _mapper.Map<Course>(CourseDto);
            await CourseRepo.AddAsync(Course);
            await _unitOfWork.SaveAsync();
            return CourseDto;
        }

        public async Task<Message> DeleteCourse(Guid CourseId,string Email) 
        {
            Teacher Teacher=await CheckIfTeacherIsOwner(Email);
            var Course = await _unitOfWork.GetRepository<Course, Guid>().GetByIdAsync(CourseId) ?? throw new CoursesOrLessonNotFoundException("This Course is Not Found");
            if (Course.TeacherId == Teacher.Id)
            {
                _unitOfWork.GetRepository<Course, Guid>().Remove(Course);
                await _unitOfWork.SaveAsync();
                return new Message() { message = "Course Deleted Successfuly" };
            }
            throw new BadRequestException("You Are Not Authorized to make this change");


        }
        //Remove(_mapper.Map<Course>(CourseDto));

        public async Task<IEnumerable<CourseDto>> GetAllCourseAsync()
        {
           var Courses= await _unitOfWork.GetRepository<Course,Guid>().GetAllAsync();
           var CoursesDto = _mapper.Map<IEnumerable<CourseDto>>(Courses); 
           return CoursesDto;
        }

        public async Task<IEnumerable<CourseDto>> GetTeacherCoursesAsync(int TeacherId)
        {
            var Teacher= await _unitOfWork.GetRepository<Teacher,int>().GetByIdAsync(TeacherId) ?? throw new TeacherNotFoundException(TeacherId);

            var TeacherSpecification = new GetTeacherCoursesSpecification(TeacherId);
            var Courses = await _unitOfWork.GetRepository<Course, Guid>().GetAllAsync(TeacherSpecification); 
            if (Courses.Count()==0)
                throw new CoursesOrLessonNotFoundException($"There is no Courses");

            var TeacherCoursesDto = _mapper.Map<IEnumerable<CourseDto>>(Courses);
            return TeacherCoursesDto;
        }

        

        public async Task<CourseDto> GetCourseInformationsAsync(Guid CourseId)
        {
            var Course = await _unitOfWork.GetRepository<Course, Guid>().GetByIdAsync(CourseId) ?? throw new CoursesOrLessonNotFoundException($"There is no Course"); ;
            var CourseDto = _mapper.Map<CourseDto>(Course);
            return CourseDto;

        }

        public async Task<CourseDto> UpdateCourse(Guid CourseId, CourseDto CourseDto,string Email)
        {
            Teacher Teacher = await CheckIfTeacherIsOwner(Email);
            if (Teacher.Id == CourseDto.TeacherId)
            {
                var course = _mapper.Map<Course>(CourseDto);
                course.Id = CourseId;
                course.Updated = DateTime.UtcNow.Date;
                _unitOfWork.GetRepository<Course, Guid>().Update(course);
                await _unitOfWork.SaveAsync();
                return _mapper.Map<CourseDto>(course);
            }
            throw new BadRequestException("You Are Not Authorized to make this change");


        }

        private async Task<Teacher> CheckIfTeacherIsOwner(string Email)
        {
            var user = await _userManager.FindByEmailAsync(Email) ?? throw new EmailOrUserNotFoundException("User Not Found");
            var userspecification = new GetTeacherMainDataSpecification(user.Id);
            var Teacher = await _unitOfWork.GetRepository<Teacher, int>().GetByIdAsync(userspecification) ?? throw new EmailOrUserNotFoundException("User Not Found");
            return Teacher;
        }
    }
}
