using CourseApplications.Models;
using CourseApplications.Repository;
using Xunit;
using FluentAssertions;
using System;

namespace CourseApplications.Test.Repository
{
    public class SubscriptionRepositoryTest
    {
        [Theory]
        [InlineData("02b20b26-d123-445d-9ea0-f68b6ad65253", "057c53f9-2f28-4e2f-99ff-733d0c7abef1")]
        [InlineData("962c1246-31c7-4e72-841c-3499786a8a10", "057c53f9-2f28-4e2f-99ff-733d0c7abef1")]
        [InlineData("b5fab767-4523-4ff5-97ca-02b453b35026", "057c53f9-2f28-4e2f-99ff-733d0c7abef1")]
        public async void ShouldCreateASubscription(string candidateId, string courseId)
        {
            //Arrange
            CourseTestContext courseTestContext = new();
            SubscriptionRepository subscriptionRepository = new(courseTestContext);

            Subscription subscription = new()
            {
              CandidateId = Guid.Parse(candidateId),
              CourseId = Guid.Parse(courseId)
            };

            //Act
            var result = await subscriptionRepository.Add(subscription);
            var subscriptionSaved = await subscriptionRepository.Get(result.SubscriptionId);

            //Assert
            result.Should().Be(subscription);
            subscriptionSaved.Should().BeEquivalentTo(subscription);
        }

        [Theory]
        [InlineData("02b20b26-d123-445d-9ea0-f68b6ad65253", "057c53f9-2f28-4e2f-99ff-733d0c7abef1")]
        [InlineData("962c1246-31c7-4e72-841c-3499786a8a10", "057c53f9-2f28-4e2f-99ff-733d0c7abef1")]
        [InlineData("b5fab767-4523-4ff5-97ca-02b453b35026", "057c53f9-2f28-4e2f-99ff-733d0c7abef1")]
        public async void ShouldDeleteASubscription(string candidateId, string courseId)
        {
            //Arrange
            CourseTestContext courseTestContext = new();
            SubscriptionRepository subscriptionRepository = new(courseTestContext);

            Subscription subscription = new()
            {
              CandidateId = Guid.Parse(candidateId),
              CourseId = Guid.Parse(courseId)
            };
            //Act
            var result = await subscriptionRepository.Add(subscription);
            var userSaved = await subscriptionRepository.Get(result.SubscriptionId);
            userSaved.Should().BeEquivalentTo(subscription);
            await subscriptionRepository.Delete(result.SubscriptionId);

            //Assert
            var subscriptionOnDatabase = await subscriptionRepository.Get(result.SubscriptionId);
            subscriptionOnDatabase.Should().Be(null);
        }

        [Theory]
        [InlineData("02b20b26-d123-445d-9ea0-f68b6ad65253", "057c53f9-2f28-4e2f-99ff-733d0c7abef1")]
        [InlineData("962c1246-31c7-4e72-841c-3499786a8a10", "057c53f9-2f28-4e2f-99ff-733d0c7abef1")]
        [InlineData("b5fab767-4523-4ff5-97ca-02b453b35026", "057c53f9-2f28-4e2f-99ff-733d0c7abef1")]
        public async void ShouldUpdateASubscription(string candidateId, string courseId)
        {
            //Arrange
            var newCourseId = Guid.Parse("611f6ff8-239a-4a1e-9d11-145120a41f12");
            CourseTestContext courseTestContext = new();
            SubscriptionRepository subscriptionRepository = new(courseTestContext);

            Subscription subscription = new()
            {
              CandidateId = Guid.Parse(candidateId),
              CourseId = Guid.Parse(courseId)
            };
            //Act
            var result = await subscriptionRepository.Add(subscription);
            var subscriptionSaved = await subscriptionRepository.Get(result.SubscriptionId);
            subscriptionSaved.Should().BeEquivalentTo(subscription);
            subscriptionSaved!.CourseId = newCourseId;
            await subscriptionRepository.Update(subscriptionSaved);

            //Assert
            var courseOnDatabase = await subscriptionRepository.Get(result.SubscriptionId);
            courseOnDatabase!.CourseId.Should().Be(newCourseId);
        }

        [Theory]
        [InlineData("02b20b26-d123-445d-9ea0-f68b6ad65253", "057c53f9-2f28-4e2f-99ff-733d0c7abef1")]
        [InlineData("962c1246-31c7-4e72-841c-3499786a8a10", "057c53f9-2f28-4e2f-99ff-733d0c7abef1")]
        [InlineData("b5fab767-4523-4ff5-97ca-02b453b35026", "057c53f9-2f28-4e2f-99ff-733d0c7abef1")]
        public async void ShouldGetASubscription(string candidateId, string courseId)
        {
            //Arrange
            CourseTestContext courseTestContext = new();
            SubscriptionRepository subscriptionRepository = new(courseTestContext);

            Subscription subscription = new()
            {
              CandidateId = Guid.Parse(candidateId),
              CourseId = Guid.Parse(courseId)
            };

            //Act
            var result = await subscriptionRepository.Add(subscription);
            var subscriptionSaved = await subscriptionRepository.Get(result.SubscriptionId);

            //Assert
            subscriptionSaved.Should().BeEquivalentTo(subscription);
        }

        [Theory]
        [InlineData("02b20b26-d123-445d-9ea0-f68b6ad65253", "057c53f9-2f28-4e2f-99ff-733d0c7abef1")]
        [InlineData("962c1246-31c7-4e72-841c-3499786a8a10", "057c53f9-2f28-4e2f-99ff-733d0c7abef1")]
        [InlineData("b5fab767-4523-4ff5-97ca-02b453b35026", "057c53f9-2f28-4e2f-99ff-733d0c7abef1")]
        public async void ShouldGetAllSubscription(string candidateId, string courseId)
        {
            //Arrange
            CourseTestContext courseTestContext = new();
            SubscriptionRepository subscriptionRepository = new(courseTestContext);

            Subscription subscription = new()
            {
              CandidateId = Guid.Parse(candidateId),
              CourseId = Guid.Parse(courseId)
            };

            Subscription subscription2 = new()
            {
              CandidateId = Guid.Parse("664e4755-0445-4b78-9357-d42b5b403b09"),
              CourseId = Guid.Parse("5bf92da3-4c8b-41d8-849c-870794b67af8")
            };

            var subscriptions = new Subscription[] { subscription, subscription2 };

            //Act
            await subscriptionRepository.Add(subscription);
            await subscriptionRepository.Add(subscription2);
            var subscriptionSaved = await subscriptionRepository.GetAll();

            //Assert
            subscriptionSaved.Should().BeEquivalentTo(subscriptions);
        }
    }
}