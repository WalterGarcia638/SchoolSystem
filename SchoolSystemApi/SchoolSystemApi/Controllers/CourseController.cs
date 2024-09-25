using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolSystemApi.Model;
using SchoolSystemApi.Repository.IRepository;

namespace SchoolSystemApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseRepository _CourseRepository;
        public CourseController(ICourseRepository CourseRepository)
        {
            _CourseRepository = CourseRepository;
        }

        [HttpGet]
        public IActionResult GetCourse()
        {
            var CourseList = _CourseRepository.GetCourses();

            return Ok(CourseList);

        }

        [HttpGet("GetById")]
        public IActionResult GeCoursetById([FromQuery] int id)
        {
            var Course = _CourseRepository.GetCourse(id);

            if (Course == null)
            {
                return NotFound();
            }

            return Ok(Course);

        }

        [HttpGet("{name}", Name = "GetCourseByName")]
        public IActionResult GetCourseByName(string name)
        {
            var Course = _CourseRepository.GetCourseByName(name);

            if (Course == null)
            {
                return NotFound();
            }

            return Ok(Course);

        }

        [HttpPost]
        public IActionResult CreateCourse([FromBody] Course Course)
        {
            if (Course == null)
            {
                return BadRequest(ModelState);
            }
            if (_CourseRepository.ExistCourse(Course.Name))
            {
                ModelState.AddModelError("", "The Course is Exist");
                return StatusCode(500, ModelState);
            }

            if (!_CourseRepository.CreateCourse(Course))
            {
                ModelState.AddModelError("", $"Error Saving{Course.Name}");
                return StatusCode(500, ModelState);
            }

            return Ok();
        }

        [HttpPatch("{CourseId:int}", Name = "UpdateCourse")]
        public IActionResult UpdateCourse(int CourseId, [FromBody] Course Course)
        {
            if (Course == null || CourseId == null || Course.Id == 0)
            {
                return BadRequest(ModelState);
            }

            if (!_CourseRepository.UpdateCourse(Course))
            {
                ModelState.AddModelError("", $"Error Update {Course.Name}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [HttpDelete("{CourseId:int}", Name = "DeleteCourse")]
        public IActionResult DeleteCourse(int CourseId)
        {
            if (!_CourseRepository.ExistCourse(CourseId))
            {
                return NotFound();
            }

            var Course = _CourseRepository.GetCourse(CourseId);

            if (!_CourseRepository.DeleteCourse(Course))
            {
                ModelState.AddModelError("", $"Error Delete {CourseId}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}