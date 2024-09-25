using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolSystemApi.Model;
using SchoolSystemApi.Model.DTO;
using SchoolSystemApi.Repository.IRepository;

namespace SchoolSystemApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentRepository _StudentRepository;
        private readonly IMapper _mapper;
        public StudentController(IStudentRepository StudentRepository, IMapper mapper)
        {
            _StudentRepository = StudentRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetStudent()
        {
            var StudentList = _StudentRepository.GetStudents();
            var StudentDTOList = _mapper.Map<ICollection<GetStudentsDTO>>(StudentList);

            return Ok(StudentDTOList);

        }

        [HttpGet("GetById")]
        public IActionResult GeStudenttById([FromQuery] int id)
        {
            var Student = _StudentRepository.GetStudent(id);

            if (Student == null)
            {
                return NotFound();
            }

            return Ok(Student);

        }

        [HttpGet("{name}", Name = "GetStudentByName")]
        public IActionResult GetStudentByName(string name)
        {
            var Student = _StudentRepository.GetStudentByName(name);

            if (Student == null)
            {
                return NotFound();
            }

            return Ok(Student);

        }

        [HttpPost]
        public IActionResult CreateStudent([FromBody] StudentDTO StudentDto)
        {
            if (StudentDto == null)
            {
                return BadRequest(ModelState);
            }

            var student = _mapper.Map<Student>(StudentDto);

            if (!_StudentRepository.CreateStudent(student))
            {
                ModelState.AddModelError("", $"Error Saving{StudentDto.FirstName}");
                return StatusCode(500, ModelState);
            }

            return Ok();
        }

        /*   [HttpPatch("{StudentId:int}", Name = "GetStudentById")]
           public IActionResult UpdateStudent(int StudentId, [FromBody] Student Student)
           {
               if (Student == null || StudentId == null || Student.Id == 0)
               {
                   return BadRequest(ModelState);
               }

               if (!_StudentRepository.UpdateStudent(Student))
               {
                   ModelState.AddModelError("", $"Error Update {Student.FirstName}");
                   return StatusCode(500, ModelState);
               }
               return NoContent();
           }*/

        [HttpPatch("{StudentId:int}", Name = "GetStudentById")]
        public IActionResult UpdateStudent(int StudentId, [FromBody] UpdateStudentDTO updateStudentDTO)
        {
            if (updateStudentDTO == null || StudentId == null || updateStudentDTO.Id == 0)
            {
                return BadRequest(ModelState);
            }

            var student = _mapper.Map<Student>(updateStudentDTO);

            if (!_StudentRepository.UpdateStudent(student))
            {
                ModelState.AddModelError("", $"Error Update {student.FirstName}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [HttpDelete("{StudentId:int}", Name = "DeleteStudent")]
        public IActionResult DeleteBrand(int StudentId)
        {
            if (!_StudentRepository.ExistStudent(StudentId))
            {
                return NotFound();
            }

            var Student = _StudentRepository.GetStudent(StudentId);

            if (!_StudentRepository.DeleteStudent(Student))
            {
                ModelState.AddModelError("", $"Error Delete {StudentId}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}