using InterRapidisimoApplication.Commands;
using InterRapidisimoApplication.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace InterRapidisimoApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StudentController : ControllerBase
{
    private readonly IMediator _mediator;

    public StudentController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetStudentById(Guid id)
    {
        try
        {
            var result = await _mediator.Send(new GetStudentByIdQuery(id));
            if (result.IsFailure)
                return NotFound(result.Error);

            return Ok(result.Value);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("{id}/classmates")]
    public async Task<IActionResult> GetClassmates(Guid id)
    {
        try
        {
            var result = await _mediator.Send(new GetClassmatesQuery(id));
            if (result.IsFailure)
                return NotFound(result.Error);

            return Ok(result.Value);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
        
    }

    [HttpPost]
    public async Task<IActionResult> RegisterStudent([FromBody] RegisterStudentCommand command)
    {
        try
        {
            var result = await _mediator.Send(command);
            if (result.IsFailure)
                return BadRequest(result.Error);

            return CreatedAtAction(nameof(GetStudentById), new { id = result.Value.Id }, result.Value);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
