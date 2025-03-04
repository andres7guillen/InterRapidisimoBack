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

    [HttpGet]
    public async Task<IActionResult> GetAllStudents()
    {
        try
        {
            var result = await _mediator.Send(new GetAllStudentsQuery());

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok(result.Value);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
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

    [HttpGet("{id}/subjects")]
    public async Task<IActionResult> GetSubjectsByStudent(Guid id)
    {
        var result = await _mediator.Send(new GetSubjectsByStudentQuery(id));

        if (result.IsFailure)
            return NotFound(result.Error);

        return Ok(result.Value);
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

    [HttpPost("{studentId}/enroll-credit-program")]
    public async Task<IActionResult> EnrollInCreditProgram(Guid studentId, [FromBody] EnrollStudentInCreditProgramCommand command)
    {
        if (command == null || command.CreditProgramId == Guid.Empty)
            return BadRequest("Invalid request.");

        if (command.StudentId == Guid.Empty)
            command = new EnrollStudentInCreditProgramCommand(studentId, command.CreditProgramId);
        var result = await _mediator.Send(command);
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }

    [HttpPost("{studentId}/enroll-subject")]
    public async Task<IActionResult> EnrollInSubject(Guid studentId, [FromBody] EnrollStudentInSubjectCommand command)
    {
        if (command == null || command.SubjectId == Guid.Empty)
            return BadRequest("Invalid request.");

        if (command.StudentId == Guid.Empty)
            command = new EnrollStudentInSubjectCommand(studentId, command.SubjectId, command.ProfessorId);

        var result = await _mediator.Send(command);

        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            var result = await _mediator.Send(new DeleteStudentCommand(id));
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
