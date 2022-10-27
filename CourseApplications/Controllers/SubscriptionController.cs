using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CourseApplications.Models;
using CourseApplications.Repository;

namespace CourseApplications.Controllers;

[ApiController]
[Route("subscription")]
public class SubscriptionController : Controller
{
    private readonly ISubscriptionRepository _repository;
    private readonly ICandidateRepository _candidateRepository;

    private readonly ICourseRepository _courseRepository;

    public SubscriptionController
    (
      ISubscriptionRepository repository,
      ICandidateRepository candidateRepository,
      ICourseRepository courseRepository
    )
    {
        _repository = repository;
        _candidateRepository = candidateRepository;
        _courseRepository = courseRepository;
    }

    [HttpPost()]
    [AllowAnonymous]
    public async Task<IActionResult> CreateSubscription([FromBody] Subscription subscription)
    {
        try
        {
            subscription.SubscriptionId = Guid.NewGuid();
            var courseExist = await _courseRepository.Get(subscription.CourseId);
            if (courseExist is null) throw new InvalidOperationException("O Id do curso informado não existe.");

            var candidateExist = await _candidateRepository.Get(subscription.CandidateId);
            if (candidateExist is null) throw new InvalidOperationException("O Id do candidato informado não existe.");

            var subscriptionExist = await _repository.Get(subscription.SubscriptionId);
            if (subscriptionExist is not null) throw new InvalidOperationException("Esta inscrição já existe.");

            var output = await _repository.Add(subscription);

            return CreatedAtAction(nameof(GetSubscription), new { id = subscription.SubscriptionId }, subscription);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    // [Authorize]
    public async Task<IActionResult> DeleteSubscription(Guid id)
    {
        try
        {
            var subscriptionExist = await _repository.Get(id);
            if (subscriptionExist is null) throw new InvalidOperationException("Esta inscrição não existe");

            await _repository.Delete(id);
            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet()]
    // [Authorize]
    public async Task<IActionResult> GetAllSubscriptions()
    {
        try
        {
            var subscriptions = await _repository.GetAll();
            if (subscriptions == null) throw new InvalidOperationException("Não existem inscrições cadastradas");

            return Ok(subscriptions);
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(ex.Message);
        }
    }
    
    [HttpGet("{id}")]
    // [Authorize]
    public async Task<IActionResult> GetSubscription(string id)
    {
        try
        {
            var subscription = await _repository.Get(new Guid(id));
            if (subscription == null) throw new InvalidOperationException("Esta inscrição não existe");

            return Ok(subscription);
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPut()]
    // [Authorize]
    public async Task<IActionResult> UpdateSubscription([FromBody] Subscription subscription)
    {
        try
        {
            var subscriptionExist = await _repository.Get(subscription.SubscriptionId);
            if (subscriptionExist == null) throw new InvalidOperationException("Esta inscrição não existe");

            subscriptionExist.CourseId = subscription.CourseId;
            subscriptionExist.CandidateId = subscription.CandidateId;

            var courseExist = await _courseRepository.Get(subscription.CourseId);
            var candidateExist = await _candidateRepository.Get(subscription.CandidateId);

            if (courseExist == null || candidateExist == null) throw new InvalidOperationException("O novo Id do curso ou do candidato é inválido");

            await _repository.Update(subscriptionExist);

            var updatedSubscription = await _repository.Get(subscription.SubscriptionId);
            return Ok(updatedSubscription);
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(ex.Message);
        }

    }
}