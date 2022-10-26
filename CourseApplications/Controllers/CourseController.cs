using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CourseApplications.Models;
using CourseApplications.Repository;

namespace CourseApplications.Controllers;

[ApiController]
[Route("course")]
public class CourseController : Controller
{
    private readonly ICourseRepository _repository;
    public CourseController(ICourseRepository repository)
    {
        _repository = repository;
    }

    [HttpPost()]
    [AllowAnonymous]
    public async Task<IActionResult> CreateCourse([FromBody] Course course)
    {
        try
        {
            var courseExist = await _repository.Get(course.CourseId);
            if (courseExist is not null) throw new InvalidOperationException("Este curso já existe");

            var output = await _repository.Add(course);
            return CreatedAtAction("GetCourse", new { id = output.CourseId }, output);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    // [Authorize]
    public async Task<IActionResult> DeleteCourse(Guid id)
    {
        try
        {
            var courseExist = await _repository.Get(id);
            if (courseExist is null) throw new InvalidOperationException("Este curso não existe");

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
    public async Task<IActionResult> GetAllCourses()
    {
        try
        {
            var courses = await _repository.GetAll();
            if (courses == null) throw new InvalidOperationException("Não existem cursos cadastrados");

            return Ok(courses);
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(ex.Message);
        }
    }
    
    [HttpGet("{id}")]
    // [Authorize]
    public async Task<IActionResult> GetCourse(string id)
    {
        try
        {
            var candidate = await _repository.Get(new Guid(id));
            if (candidate == null) throw new InvalidOperationException("Este curso não existe");

            return Ok(candidate);
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPut()]
    // [Authorize]
    public async Task<IActionResult> UpdateCandidate([FromBody] Course course)
    {
        try
        {
            var courseExist = await _repository.Get(course.CourseId);
            if (courseExist == null) throw new InvalidOperationException("Este course não existe");

            courseExist.CourseName = course.CourseName;

            await _repository.Update(courseExist);

            var updatedCourse = await _repository.Get(course.CourseId);
            return Ok(updatedCourse);
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(ex.Message);
        }

    }
}