using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SchoolSystemApi.Controllers;
using SchoolSystemApi.Model;
using SchoolSystemApi.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSystemApiTest
{
    public class CourseControllerTests
    {
        private readonly Mock<ICourseRepository> _mockRepo;
        private readonly CourseController _controller;

        public CourseControllerTests()
        {
            _mockRepo = new Mock<ICourseRepository>();
            _controller = new CourseController(_mockRepo.Object);
        }

        [Fact]
        public void GetCourse_ReturnsOkResult_WithListOfCourses()
        {
            // Arrange
            var courses = new List<Course>
        {
            new Course { Id = 1, Name = "Matematicas", Description = "Curso basico de matematicas" }
        };
            _mockRepo.Setup(repo => repo.GetCourses()).Returns(courses);

            // Act
            var result = _controller.GetCourse();

            // Assert
            result.Should().BeOfType<OkObjectResult>().Which.StatusCode.Should().Be(200);
            var okResult = result as OkObjectResult;
            okResult.Value.Should().BeEquivalentTo(courses);
        }

        [Fact]
        public void GetCourseById_ReturnsNotFound_WhenCourseDoesNotExist()
        {
            // Arrange
            _mockRepo.Setup(repo => repo.GetCourse(It.IsAny<int>())).Returns((Course)null);

            // Act
            var result = _controller.GeCoursetById(1);

            // Assert
            result.Should().BeOfType<NotFoundResult>().Which.StatusCode.Should().Be(404);
        }

        [Fact]
        public void GetCourseById_ReturnsOk_WhenCourseExists()
        {
            // Arrange
            var course = new Course { Id = 1, Name = "Matematicas", Description = "Curso basico de matematicas" };
            _mockRepo.Setup(repo => repo.GetCourse(1)).Returns(course);

            // Act
            var result = _controller.GeCoursetById(1);

            // Assert
            result.Should().BeOfType<OkObjectResult>().Which.StatusCode.Should().Be(200);
        }

        [Fact]
        public void GetCourseByName_ReturnsNotFound_WhenCourseDoesNotExist()
        {
            // Arrange
            _mockRepo.Setup(repo => repo.GetCourseByName(It.IsAny<string>())).Returns((Course)null);

            // Act
            var result = _controller.GetCourseByName("Curso basico de ciencias");

            // Assert
            result.Should().BeOfType<NotFoundResult>().Which.StatusCode.Should().Be(404);
        }

        [Fact]
        public void GetCourseByName_ReturnsOk_WhenCourseExists()
        {
            // Arrange
            var course = new Course { Id = 1, Name = "Matematicas", Description = "Curso basico de matematicas" };
            _mockRepo.Setup(repo => repo.GetCourseByName("Matematicas")).Returns(course);

            // Act
            var result = _controller.GetCourseByName("Matematicas");

            // Assert
            result.Should().BeOfType<OkObjectResult>().Which.StatusCode.Should().Be(200);
        }

        [Fact]
        public void CreateCourse_ReturnsBadRequest_WhenModelIsInvalid()
        {
            // Act
            var result = _controller.CreateCourse(null);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>().Which.StatusCode.Should().Be(400);
        }

        [Fact]
        public void CreateCourse_ReturnsOk_WhenCreationIsSuccessful()
        {
            // Arrange
            var course = new Course { Id = 1, Name = "Matematicas", Description = "Curso basico de matematicas" };
            _mockRepo.Setup(repo => repo.CreateCourse(course)).Returns(true);
            _mockRepo.Setup(repo => repo.ExistCourse(It.IsAny<string>())).Returns(false);

            // Act
            var result = _controller.CreateCourse(course);

            // Assert
            result.Should().BeOfType<OkResult>().Which.StatusCode.Should().Be(200);
        }

        [Fact]
        public void UpdateCourse_ReturnsNoContent_WhenUpdateIsSuccessful()
        {
            // Arrange
            var course = new Course { Id = 1, Name = "Matematicas", Description = "Curso basico de matematicas" };
            _mockRepo.Setup(repo => repo.UpdateCourse(course)).Returns(true);

            // Act
            var result = _controller.UpdateCourse(1, course);

            // Assert
            result.Should().BeOfType<NoContentResult>().Which.StatusCode.Should().Be(204);
        }

        [Fact]
        public void DeleteCourse_ReturnsNoContent_WhenDeleteIsSuccessful()
        {
            // Arrange
            var course = new Course { Id = 1, Name = "Matematicas", Description = "Curso basico de matematicas" };
            _mockRepo.Setup(repo => repo.ExistCourse(1)).Returns(true);
            _mockRepo.Setup(repo => repo.GetCourse(1)).Returns(course);
            _mockRepo.Setup(repo => repo.DeleteCourse(course)).Returns(true);

            // Act
            var result = _controller.DeleteCourse(1);

            // Assert
            result.Should().BeOfType<NoContentResult>().Which.StatusCode.Should().Be(204);
        }

        [Fact]
        public void DeleteCourse_ReturnsNotFound_WhenCourseDoesNotExist()
        {
            // Arrange
            _mockRepo.Setup(repo => repo.ExistCourse(It.IsAny<int>())).Returns(false);

            // Act
            var result = _controller.DeleteCourse(1);

            // Assert
            result.Should().BeOfType<NotFoundResult>().Which.StatusCode.Should().Be(404);
        }
    }
}