using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SchoolSystemApi.Controllers;
using SchoolSystemApi.Model.DTO;
using SchoolSystemApi.Model;
using SchoolSystemApi.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;

namespace SchoolSystemApiTest
{
    public class StudentControllerTests
    {
        private readonly Mock<IStudentRepository> _mockRepo;
        private readonly Mock<IMapper> _mockMapper;
        private readonly StudentController _controller;

        public StudentControllerTests()
        {
            _mockRepo = new Mock<IStudentRepository>();
            _mockMapper = new Mock<IMapper>();
            _controller = new StudentController(_mockRepo.Object, _mockMapper.Object);
        }

        [Fact]
        public void GetStudent_ReturnsOkResult_WithListOfStudents()
        {
            // Arrange
            var students = new List<Student> { new Student { Id = 1, FirstName = "John", LastName = "Doe" } };
            var studentsDTO = new List<GetStudentsDTO> { new GetStudentsDTO { Id = 1, FirstName = "John", LastName = "Doe" } };

            _mockRepo.Setup(repo => repo.GetStudents()).Returns(students);
            _mockMapper.Setup(mapper => mapper.Map<ICollection<GetStudentsDTO>>(students)).Returns(studentsDTO);

            // Act
            var result = _controller.GetStudent();

            // Assert
            result.Should().BeOfType<OkObjectResult>().Which.StatusCode.Should().Be(200);
            var okResult = result as OkObjectResult;
            okResult.Value.Should().BeEquivalentTo(studentsDTO);
        }

        [Fact]
        public void GetStudentById_ReturnsNotFound_WhenStudentDoesNotExist()
        {
            // Arrange
            _mockRepo.Setup(repo => repo.GetStudent(It.IsAny<int>())).Returns((Student)null);

            // Act
            var result = _controller.GeStudenttById(1);

            // Assert
            result.Should().BeOfType<NotFoundResult>().Which.StatusCode.Should().Be(404);
        }

        [Fact]
        public void GetStudentByName_ReturnsOk_WhenStudentExists()
        {
            // Arrange
            var student = new Student { Id = 1, FirstName = "John", LastName = "Doe" };
            _mockRepo.Setup(repo => repo.GetStudentByName("John")).Returns(student);

            // Act
            var result = _controller.GetStudentByName("John");

            // Assert
            result.Should().BeOfType<OkObjectResult>().Which.StatusCode.Should().Be(200);
        }

        [Fact]
        public void CreateStudent_ReturnsBadRequest_WhenModelIsInvalid()
        {
            // Act
            var result = _controller.CreateStudent(null);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>().Which.StatusCode.Should().Be(400);
        }

        [Fact]
        public void UpdateStudent_ReturnsNoContent_WhenUpdateIsSuccessful()
        {
            // Arrange
            var updateStudentDTO = new UpdateStudentDTO { Id = 1, FirstName = "John", LastName = "Doe" };
            var student = new Student { Id = 1, FirstName = "John", LastName = "Doe" };

            _mockMapper.Setup(mapper => mapper.Map<Student>(updateStudentDTO)).Returns(student);
            _mockRepo.Setup(repo => repo.UpdateStudent(student)).Returns(true);

            // Act
            var result = _controller.UpdateStudent(1, updateStudentDTO);

            // Assert
            result.Should().BeOfType<NoContentResult>().Which.StatusCode.Should().Be(204);
        }

        [Fact]
        public void DeleteStudent_ReturnsNoContent_WhenDeleteIsSuccessful()
        {
            // Arrange
            var student = new Student { Id = 1, FirstName = "John", LastName = "Doe" };

            _mockRepo.Setup(repo => repo.ExistStudent(1)).Returns(true);
            _mockRepo.Setup(repo => repo.GetStudent(1)).Returns(student);
            _mockRepo.Setup(repo => repo.DeleteStudent(student)).Returns(true);

            // Act
            var result = _controller.DeleteBrand(1);

            // Assert
            result.Should().BeOfType<NoContentResult>().Which.StatusCode.Should().Be(204);
        }
    }
}