using AutoMapper;
using WebApi.Dtos;
using WebApi.Models;

namespace WebApi.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
            CreateMap<Course, CourseDto>();
            CreateMap<CourseDto, Course>();
            CreateMap<Lesson, LessonDto>();
            CreateMap<LessonDto, Lesson>();
            CreateMap<Payment, PaymentDto>();
            CreateMap<PaymentDto, Payment>();
        }
    }
}
