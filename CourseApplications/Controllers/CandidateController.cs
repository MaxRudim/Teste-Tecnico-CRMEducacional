using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CourseApplications.Models;
using CourseApplications.Repository;

namespace CourseApplications.Controllers;

[ApiController]
[Route("candidate")]
public class CandidateController : Controller
{
    private readonly ICandidateRepository _repository;
    public CandidateController(ICandidateRepository repository)
    {
        _repository = repository;
    }
    // [HttpPost("Authentication")]
    // public async Task<IActionResult> Authenticate([FromBody] LoginData loginData)
    // {
    //     try
    //     {
    //         var token = await _service.Authenticate(loginData);
    //         return Ok(token);
    //     }
    //     catch (InvalidOperationException ex)
    //     {
    //         return BadRequest(ex.Message);
    //     }
    // }

    [HttpPost()]
    [AllowAnonymous]
    public async Task<IActionResult> CreateCandidate([FromBody] Candidate candidate)
    {
        try
        {
            var candidateExist = await _repository.GetByEmail(candidate.Email);
            if (candidateExist is not null) throw new InvalidOperationException("Candidato já existente");

            var output = await _repository.Add(candidate);
            return CreatedAtAction("GetCandidate", new { id = output.CandidateId }, output);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    // [Authorize]
    public async Task<IActionResult> DeleteCandidate(Guid id)
    {
        try
        {
            var candidateExist = await _repository.Get(id);
            if (candidateExist is null) throw new InvalidOperationException("O candidato não existe");

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
    public async Task<IActionResult> GetAllCandidates()
    {
        try
        {
            var candidates = await _repository.GetAll();
            if (candidates == null) throw new InvalidOperationException("Não existem candidados cadastrados");

            return Ok(candidates);
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(ex.Message);
        }
    }
    
    [HttpGet("{id}")]
    // [Authorize]
    public async Task<IActionResult> GetCandidate(string id)
    {
        try
        {
            var candidate = await _repository.Get(new Guid(id));
            if (candidate == null) throw new InvalidOperationException("O candidato não existe");

            return Ok(candidate);
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPut()]
    // [Authorize]
    public async Task<IActionResult> UpdateCandidate([FromBody] Candidate candidate)
    {
        try
        {
            var candidateExist = await _repository.Get(candidate.CandidateId);
            if (candidateExist == null) throw new InvalidOperationException("O candidato não existe");

            candidateExist.Cpf = candidate.Cpf;
            candidateExist.Email = candidate.Email;
            candidateExist.Password = candidate.Password;


            await _repository.Update(candidateExist);

            var updatedCandidate = await _repository.Get(candidate.CandidateId);
            return Ok(updatedCandidate);
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(ex.Message);
        }

    }
}