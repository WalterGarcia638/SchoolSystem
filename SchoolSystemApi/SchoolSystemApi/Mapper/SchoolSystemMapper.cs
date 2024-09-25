using AutoMapper;
using SchoolSystemApi.Model;
using SchoolSystemApi.Model.DTO;
using System.Runtime.InteropServices;

namespace SchoolSystemApi.Mapper
{
    public class SchoolSystemMapper : Profile
    {
        public  SchoolSystemMapper() 
        {
            CreateMap<Student, StudentDTO>().ReverseMap();
            CreateMap<Student, UpdateStudentDTO>().ReverseMap();
            CreateMap<Student, GetStudentsDTO>()
            .ForMember(dest => dest.CourseName, opt => opt.MapFrom(src => src.Course.Name));
        }
        
    }
}
