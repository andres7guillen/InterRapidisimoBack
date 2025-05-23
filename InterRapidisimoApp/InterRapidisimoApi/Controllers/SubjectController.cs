﻿using InterRapidisimoApplication.Commands;
using InterRapidisimoApplication.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InterRapidisimoApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SubjectController : ControllerBase
{
    private readonly IMediator _mediator;

    public SubjectController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetSubjectById(Guid id)
    {
        try
        {
            var result = await _mediator.Send(new GetSubjectByIdQuery(id));
            if (result.IsFailure)
                return NotFound(result.Error);

            return Ok(result.Value);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetAllSubjects()
    {
        var query = new GetAllSubjectsQuery();
        var result = await _mediator.Send(query);
        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }
        return BadRequest(result.Error);
    }

    [HttpGet("{id}/students")]
    public async Task<IActionResult> GetStudentsInSubject(Guid id)
    {
        try
        {
            var result = await _mediator.Send(new GetStudentsBySubjectQuery(id));
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
    public async Task<IActionResult> CreateSubject([FromBody] CreateSubjectCommand command)
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
            var result = await _mediator.Send(new DeleteSubjectCommand(id));
            if (result.IsSuccess)
                return Ok(result.Value);
            return BadRequest(result.Error);
        }
        catch (Exception)
        {
            throw;
        }
    }
}
