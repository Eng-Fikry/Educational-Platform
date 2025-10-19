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
    public class LessonService(IUnitOfWork _unitOfWork,IMapper _mapper,UserManager<User> _userManager) : ILessonService
    {
        public async Task<CourseLessonsDto> AddLessonsAsync(CourseLessonsDto courseLessons,string Email)
        {
            Teacher teacher=await CheckIfTeacherIsOwner(Email);
            var Course = await _unitOfWork.GetRepository<Course, Guid>().GetByIdAsync(courseLessons.CourseId) ?? throw new CoursesOrLessonNotFoundException("There is no Courses");
            if (teacher.Id == Course.TeacherId)
            {
                var Lesson = _unitOfWork.GetRepository<Lesson, Guid>();
                var Lessons = courseLessons.Lessons;
                var addedlesson = new Lesson();
                var ArrayOfLessons = new List<Lesson>();
                addedlesson = AddLessonsForCourse(courseLessons, Lessons, addedlesson, ArrayOfLessons);
                await Lesson.AddRangeAsync(ArrayOfLessons);
                await _unitOfWork.SaveAsync();

                return await GetLessonsAsync(courseLessons.CourseId);
            }
            throw new BadRequestException("Your are Not The Teacher");
        }


        public async Task<Message> DeleteLessonAsync(Guid LessonId,string Email)
        {
            Teacher Teacher = await CheckIfTeacherIsOwner(Email);
            var Lesson=await _unitOfWork.GetRepository<Lesson,Guid>().GetByIdAsync(LessonId) ?? throw new CoursesOrLessonNotFoundException("This Lesson is Not Found");
            var Course = await _unitOfWork.GetRepository<Course, Guid>().GetByIdAsync(Lesson.CourseId) ?? throw new CoursesOrLessonNotFoundException("This Course is Not Found");
            if (Course.TeacherId == Teacher.Id)
            {
                _unitOfWork.GetRepository<Lesson, Guid>().Remove(Lesson);
                await _unitOfWork.SaveAsync();
                return new Message()
                {
                    message = $"Lesson {Lesson.Title} is deleted succfuly"
                };
            }
            throw new BadRequestException(message: "You Are Not Authorized to make this change");


        }

        public async Task<CourseLessonsDto> GetLessonsAsync(Guid CourseId)
        {
            var Course = await _unitOfWork.GetRepository<Course,Guid>().GetByIdAsync(CourseId) ?? throw new CoursesOrLessonNotFoundException($"There is no Course to show its lessons"); ;
            var Lesson = _unitOfWork.GetRepository<Lesson, Guid>();
            var LessonsSpecification = new GetCourseLessonsSpecification(CourseId);
            var ReturnedLessons = await Lesson.GetAllAsync(LessonsSpecification);
            var ReturnedLessonsDto = _mapper.Map<IEnumerable<LessonDto>>(ReturnedLessons);
            var CourseLessons = new CourseLessonsDto()
            {
                CourseId = CourseId,
                Lessons = ReturnedLessonsDto
            };
            return CourseLessons;
        }

        public async Task<LessonDto> UpdateLessonAsync(Guid LessonId, LessonDto lesson, string Email)
        {
            Teacher Teacher=await CheckIfTeacherIsOwner(Email);
            var Lesson = await _unitOfWork.GetRepository<Lesson, Guid>().GetByIdAsync(LessonId) ?? throw new CoursesOrLessonNotFoundException("This Lesson is Not Found");
            var Course=await _unitOfWork.GetRepository<Course, Guid>().GetByIdAsync(Lesson.CourseId) ?? throw new CoursesOrLessonNotFoundException("This Course is Not Found");
            //Lesson = _mapper.Map<Lesson>(lesson);
            if (Course.TeacherId == Teacher.Id)
            {
                _mapper.Map(lesson, Lesson);
                Course.Updated = DateTime.UtcNow;
                Lesson.Id = LessonId;
                _unitOfWork.GetRepository<Lesson, Guid>().Update(Lesson);
                await _unitOfWork.SaveAsync();
                return _mapper.Map<LessonDto>(Lesson);
            }
            throw new BadRequestException(message: "You Are Not Authorized to make this change");
        }

        private static Lesson AddLessonsForCourse(CourseLessonsDto courseLessons, IEnumerable<LessonDto> Lessons, Lesson addedlesson, List<Lesson> ArrayOfLessons)
        {
            foreach (var lesson in Lessons)
            {
                addedlesson = new Lesson()
                {
                    Id = lesson.Id,
                    Title = lesson.Title,
                    Description = lesson.Description,
                    Thumbnail = lesson.Thumbnail,
                    Order = lesson.Order,
                    VideoUrl = lesson.VideoUrl,
                    CourseId = courseLessons.CourseId,

                };
                ArrayOfLessons.Add(addedlesson);
            }

            return addedlesson;
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
