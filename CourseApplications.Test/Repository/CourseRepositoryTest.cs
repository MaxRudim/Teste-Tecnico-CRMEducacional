using CourseApplications.Models;
using CourseApplications.Repository;
using Xunit;
using FluentAssertions;
using System;

namespace CourseApplications.Test.Repository
{
    public class CourseRepositoryTest
    {
        [Theory]
        [InlineData("Curso de Desenvolvimento Web", "057c53f9-2f28-4e2f-99ff-733d0c7abef1")]
        [InlineData("Curso de Engenharia Civil", "057c53f9-2f28-4e2f-99ff-733d0c7abef1")]
        [InlineData("Curso de Medicina", "057c53f9-2f28-4e2f-99ff-733d0c7abef1")]
        public async void ShouldCreateACourse(string courseName, string subscriptionId)
        {
            //Arrange
            CourseTestContext courseTestContext = new();
            CourseRepository courseRepository = new(courseTestContext);

            Course course = new() { CourseName = courseName, SubscriptionId = Guid.Parse(subscriptionId) };

            //Act
            var result = await courseRepository.Add(course);
            var candidateSaved = await courseRepository.Get(result.CourseId);

            //Assert
            result.Should().Be(course);
            candidateSaved.Should().BeEquivalentTo(course);
        }

        [Theory]
        [InlineData("Curso de Desenvolvimento Web", "057c53f9-2f28-4e2f-99ff-733d0c7abef1")]
        [InlineData("Curso de Engenharia Civil", "057c53f9-2f28-4e2f-99ff-733d0c7abef1")]
        [InlineData("Curso de Medicina", "057c53f9-2f28-4e2f-99ff-733d0c7abef1")]
        public async void ShouldDeleteACourse(string courseName, string subscriptionId)
        {
            //Arrange
            CourseTestContext courseTestContext = new();
            CourseRepository courseRepository = new(courseTestContext);

            Course course = new() { CourseName = courseName, SubscriptionId = Guid.Parse(subscriptionId) };

            //Act
            var result = await courseRepository.Add(course);
            var userSaved = await courseRepository.Get(result.CourseId);
            userSaved.Should().BeEquivalentTo(course);
            await courseRepository.Delete(result.CourseId);

            //Assert
            var courseOnDatabase = await courseRepository.Get(result.CourseId);
            courseOnDatabase.Should().Be(null);
        }

        [Theory]
        [InlineData("Curso de Desenvolvimento Web", "057c53f9-2f28-4e2f-99ff-733d0c7abef1")]
        [InlineData("Curso de Engenharia Civil", "057c53f9-2f28-4e2f-99ff-733d0c7abef1")]
        [InlineData("Curso de Medicina", "057c53f9-2f28-4e2f-99ff-733d0c7abef1")]
        public async void ShouldUpdateACourse(string courseName, string subscriptionId)
        {
            //Arrange
            var newCourseName = "Curso de Programação";
            CourseTestContext courseTestContext = new();
            CourseRepository courseRepository = new(courseTestContext);

            Course course = new() { CourseName = courseName, SubscriptionId = Guid.Parse(subscriptionId) };

            //Act
            var result = await courseRepository.Add(course);
            var courseSaved = await courseRepository.Get(result.CourseId);
            courseSaved.Should().BeEquivalentTo(course);
            courseSaved!.CourseName = newCourseName;
            await courseRepository.Update(courseSaved);

            //Assert
            var courseOnDatabase = await courseRepository.Get(result.CourseId);
            courseOnDatabase!.CourseName.Should().Be(newCourseName);
        }

        [Theory]
        [InlineData("Curso de Desenvolvimento Web", "057c53f9-2f28-4e2f-99ff-733d0c7abef1")]
        [InlineData("Curso de Engenharia Civil", "057c53f9-2f28-4e2f-99ff-733d0c7abef1")]
        [InlineData("Curso de Medicina", "057c53f9-2f28-4e2f-99ff-733d0c7abef1")]
        public async void ShouldGetACourse(string courseName, string subscriptionId)
        {
            //Arrange
            CourseTestContext courseTestContext = new();
            CourseRepository courseRepository = new(courseTestContext);

            Course course = new() { CourseName = courseName, SubscriptionId = Guid.Parse(subscriptionId) };

            //Act
            var result = await courseRepository.Add(course);
            var courseSaved = await courseRepository.Get(result.CourseId);

            //Assert
            courseSaved.Should().BeEquivalentTo(course);
        }

        [Theory]
        [InlineData("Curso de Desenvolvimento Web", "057c53f9-2f28-4e2f-99ff-733d0c7abef1")]
        [InlineData("Curso de Engenharia Civil", "057c53f9-2f28-4e2f-99ff-733d0c7abef1")]
        [InlineData("Curso de Medicina", "057c53f9-2f28-4e2f-99ff-733d0c7abef1")]
        public async void ShouldGetAllCandidates(string courseName, string subscriptionId)
        {
            //Arrange
            CourseTestContext courseTestContext = new();
            CourseRepository courseRepository = new(courseTestContext);

            Course course = new() { CourseName = courseName, SubscriptionId = Guid.Parse(subscriptionId) };
            Course course2 = new() { CourseName = "Curso de Oceanologia", SubscriptionId = Guid.Parse("560d284e-b21f-4fc2-8cc3-59f5f4df42d8") };

            var courses = new Course[] { course, course2 };

            //Act
            await courseRepository.Add(course);
            await courseRepository.Add(course2);
            var coursesSaved = await courseRepository.GetAll();

            //Assert
            coursesSaved.Should().BeEquivalentTo(courses);

        }
    }
}