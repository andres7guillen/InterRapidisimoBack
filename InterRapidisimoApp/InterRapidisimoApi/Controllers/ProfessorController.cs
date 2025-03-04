using InterRapidisimoApi.Models;
using InterRapidisimoApplication.Commands;
using InterRapidisimoApplication.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InterRapidisimoApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProfessorController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProfessorController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllProfessors()
    {
        try
        {
            var query = new GetAllProfessorsQuery();
            var result = await _mediator.Send(query);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return BadRequest(result.Error);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
            throw;
        }
        
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProfessorById(Guid id)
    {
        try
        {
            var result = await _mediator.Send(new GetProfessorByIdQuery(id));
            if (result.IsFailure)
                return NotFound(result.Error);

            return Ok(result.Value);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
        
    }

    [HttpGet("{id}/subjects")]
    public async Task<IActionResult> GetSubjectsByProfessor(Guid id)
    {
        try
        {
            var result = await _mediator.Send(new GetSubjectsByProfessorQuery(id));
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
    public async Task<IActionResult> CreateProfessor([FromBody] CreateProfessorCommand command)
    {
        try
        {
            var result = await _mediator.Send(command);
            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok(result.Value);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id) 
    {
        try
        {
            var result = await _mediator.Send(new DeleteProfessorCommand(id));
            if(result.IsSuccess)
                return Ok(result.Value);
            return BadRequest(result.Error);
        }
        catch (Exception)
        {
            throw;
        }        
    }

    [HttpPost("assign-subject")]
    public async Task<IActionResult> AssignSubjectToProfessor([FromBody] AssignSubjectToProfessorRequestModel request)
    {
        var command = new AssignSubjectToProfessorCommand(request.ProfessorId, request.SubjectId);
        var result = await _mediator.Send(command);

        if (result.IsFailure)
            return BadRequest(result.Error);

        return Ok(result.IsSuccess);
    }

    [HttpGet("by-subject/{subjectId}")]
    public async Task<IActionResult> GetProfessorsBySubject(Guid subjectId)
    {
        var query = new GetProfessorsBySubjectQuery(subjectId);
        var result = await _mediator.Send(query);

        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }
}
