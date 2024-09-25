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
        }
        
    }
}
