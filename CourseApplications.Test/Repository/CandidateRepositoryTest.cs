using CourseApplications.Models;
using CourseApplications.Repository;
using Xunit;
using FluentAssertions;

namespace CourseApplications.Test.Repository
{
    public class CandidateRepositoryTest
    {
        [Theory]
        [InlineData("email@email.com", "123456", "435.861.890-10")]
        [InlineData("crm@cmr.com", "123456", "536.766.360-58")]
        [InlineData("teste@teste.com", "123456", "180.333.500-91")]
        public async void ShouldCreateACandidate(string email, string password, string cpf)
        {
            //Arrange
            CourseTestContext courseTestContext = new();
            CandidateRepository candidateRepository = new(courseTestContext);

            Candidate candidate = new() { Email = email, Password = password, Cpf = cpf };

            //Act
            var result = await candidateRepository.Add(candidate);
            var candidateSaved = await candidateRepository.Get(result.CandidateId);

            //Assert
            result.Should().Be(candidate);
            candidateSaved.Should().BeEquivalentTo(candidate);
        }

        [Theory]
        [InlineData("email@email.com", "123456", "435.861.890-10")]
        [InlineData("crm@cmr.com", "123456", "536.766.360-58")]
        [InlineData("teste@teste.com", "123456", "180.333.500-91")]
        public async void ShouldDeleteACandidate(string email, string password, string cpf)
        {
            //Arrange
            CourseTestContext courseTestContext = new();
            CandidateRepository candidateRepository = new(courseTestContext);

            Candidate candidate = new() { Email = email, Password = password, Cpf = cpf };

            //Act
            var result = await candidateRepository.Add(candidate);
            var candidateSaved = await candidateRepository.Get(result.CandidateId);
            candidateSaved.Should().BeEquivalentTo(candidate);
            await candidateRepository.Delete(result.CandidateId);

            //Assert
            var userOnDatabase = await candidateRepository.Get(result.CandidateId);
            userOnDatabase.Should().Be(null);

        }

        [Theory]
        [InlineData("email@email.com", "123456", "435.861.890-10")]
        [InlineData("crm@cmr.com", "123456", "536.766.360-58")]
        [InlineData("teste@teste.com", "123456", "180.333.500-91")]
        public async void ShouldUpdateACandidate(string email, string password, string cpf)
        {
            //Arrange
            var novoPassword = "novoPassword";
            CourseTestContext courseTestContext = new();
            CandidateRepository candidateRepository = new(courseTestContext);

            Candidate candidate = new() { Email = email, Password = password, Cpf = cpf };

            //Act
            var result = await candidateRepository.Add(candidate);
            var candidateSaved = await candidateRepository.Get(result.CandidateId);
            candidateSaved.Should().BeEquivalentTo(candidate);
            candidateSaved!.Password = novoPassword;
            await candidateRepository.Update(candidateSaved);

            //Assert
            var candidateOnDatabase = await candidateRepository.Get(result.CandidateId);
            candidateOnDatabase!.Password.Should().Be(novoPassword);
        }

        [Theory]
        [InlineData("email@email.com", "123456", "435.861.890-10")]
        [InlineData("crm@cmr.com", "123456", "536.766.360-58")]
        [InlineData("teste@teste.com", "123456", "180.333.500-91")]
        public async void ShouldGetACandidate(string email, string password, string cpf)
        {
            //Arrange
            CourseTestContext courseTestContext = new();
            CandidateRepository candidateRepository = new(courseTestContext);

            Candidate candidate = new() { Email = email, Password = password, Cpf = cpf };

            //Act
            var result = await candidateRepository.Add(candidate);
            var userSaved = await candidateRepository.Get(result.CandidateId);

            //Assert
            userSaved.Should().BeEquivalentTo(candidate);
        }

        [Theory]
        [InlineData("email@email.com", "123456", "435.861.890-10")]
        [InlineData("crm@cmr.com", "123456", "536.766.360-58")]
        [InlineData("teste@teste.com", "123456", "180.333.500-91")]
        public async void ShouldGetAllCandidates(string email, string password, string cpf)
        {
            //Arrange
            CourseTestContext courseTestContext = new();
            CandidateRepository candidateRepository = new(courseTestContext);

            Candidate candidate = new() { Email = email, Password = password, Cpf = cpf };
            Candidate candidate2 = new() { Email = "outroemail@gmail.com", Password = password, Cpf = "128.502.040-56" };

            var candidates = new Candidate[] { candidate, candidate2 };

            //Act
            await candidateRepository.Add(candidate);
            await candidateRepository.Add(candidate2);
            var candidatesSaved = await candidateRepository.GetAll();

            //Assert
            candidatesSaved.Should().BeEquivalentTo(candidates);

        }
    }
}