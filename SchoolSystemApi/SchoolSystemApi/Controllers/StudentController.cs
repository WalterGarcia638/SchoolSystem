using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolSystemApi.Model;
using SchoolSystemApi.Repository.IRepository;

namespace SchoolSystemApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentRepository _StudentRepository;
        public StudentController(IStudentRepository StudentRepository)
        {
            _StudentRepository = StudentRepository;
        }

        [HttpGet]
        public IActionResult GetStudent()
        {
            var StudentList = _StudentRepository.GetStudents();

            return Ok(StudentList);

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
        public IActionResult CreateStudent([FromBody] Student Student)
        {
            if (Student == null)
            {
                return BadRequest(ModelState);
            }
           /* if (_StudentRepository.ExistStudent(Student.Name))
            {
                ModelState.AddModelError("", "The Student is Exist");
                return StatusCode(500, ModelState);
            }*/

            if (!_StudentRepository.CreateStudent(Student))
            {
                ModelState.AddModelError("", $"Error Saving{Student.FirstName}");
                return StatusCode(500, ModelState);
            }

            return Ok();
        }

        [HttpPatch("{StudentId:int}", Name = "GetStudentById")]
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