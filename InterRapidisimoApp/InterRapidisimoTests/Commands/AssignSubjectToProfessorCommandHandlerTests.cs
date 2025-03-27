using Azure.Core;
using CSharpFunctionalExtensions;
using InterRapidisimoApplication.Commands;
using InterRapidisimoDomain.Entities;
using InterRapidisimoDomain.Repositories;
using Moq;

namespace InterRapidisimoTests.Commands;

public class AssignSubjectToProfessorCommandHandlerTests
{
    private readonly Mock<IProfessorRepository> _professorRepositoryMock;
    private readonly Mock<ISubjectRepository> _subjectRepositoryMock;
    private readonly AssignSubjectToProfessorCommand.AssignSubjectToProfessorCommandHandler _handler;

    public AssignSubjectToProfessorCommandHandlerTests()
    {
        _professorRepositoryMock = new Mock<IProfessorRepository>();
        _subjectRepositoryMock = new Mock<ISubjectRepository>();
        _handler = new AssignSubjectToProfessorCommand.AssignSubjectToProfessorCommandHandler(
            _professorRepositoryMock.Object,
            _subjectRepositoryMock.Object
        );
    }

    [Fact]
    public async Task Handle_ProfessorNotFound_ShouldReturnFailure()
    {
        // Arrange
        var command = new AssignSubjectToProfessorCommand(Guid.NewGuid(), Guid.NewGuid());
        _professorRepositoryMock.Setup(repo => repo.GetById(command.ProfessorId))
            .ReturnsAsync(Maybe<Professor>.None);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Equal("Professor not found.", result.Error);
    }

    [Fact]
    public async Task Handle_SubjectNotFound_ShouldReturnFailure()
    {
        // Arrange
        var command = new AssignSubjectToProfessorCommand(Guid.NewGuid(), Guid.NewGuid());
        _subjectRepositoryMock.Setup(repo => repo.GetById(command.SubjectId))
            .ReturnsAsync(Maybe<Subject>.None);
        var professor = Professor.Create(name: "Andres", surname: "Guillen", email: "andres7guillen@gmail.com");
        _professorRepositoryMock.Setup(repo => repo.GetById(command.ProfessorId))
            .ReturnsAsync(Maybe<Professor>.From(professor.Value));

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Equal("Subject not found.", result.Error);
    }

    [Fact]
    public async Task Handle_ProfessorsLimitReached_ShouldReturnFailure()
    {
        // Arrange
        var professor = Professor.Create(name: "Andres", surname: "Guillén", email: "andres7guillen@gmail.com").Value;
        List<Subject> subjects = new List<Subject>();
        var maths = Subject.CreateSubject("Maths").Value;
        professor.AssignSubject(maths);
        var command = new AssignSubjectToProfessorCommand(Guid.NewGuid(), Guid.NewGuid());

        _subjectRepositoryMock.Setup(repo => repo.GetById(command.SubjectId))
            .ReturnsAsync(Maybe<Subject>.From(maths));

        _professorRepositoryMock.Setup(repo => repo.GetById(command.ProfessorId))
            .ReturnsAsync(Maybe<Professor>.From(professor));

        _professorRepositoryMock.Setup(repo => repo.CountWithTwoSubjectsAsync())
            .ReturnsAsync(Result.Success(5)); // Límite alcanzado

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Equal("Already there are 5 professors teaching 2 subjects", result.Error);
    }

    [Fact]
    public async Task Handle_AssignSubjectSuccessfully_ShouldReturnSuccess()
    {
        // Arrange
        var professor = Professor.Create(name: "Andrés", surname: "Guillén", email: "andres7guillen@gmail.com").Value;
        var subject = Subject.CreateSubject(name: "Maths").Value;
        var command = new AssignSubjectToProfessorCommand(Guid.NewGuid(), Guid.NewGuid());

        _professorRepositoryMock.Setup(repo => repo.GetById(command.ProfessorId))
            .ReturnsAsync(Maybe<Professor>.From(professor));

        _subjectRepositoryMock.Setup(repo => repo.GetById(command.SubjectId))
            .ReturnsAsync(Maybe<Subject>.From(subject));

        _professorRepositoryMock.Setup(repo => repo.CountWithTwoSubjectsAsync())
            .ReturnsAsync(Result.Success(2));

        _professorRepositoryMock.Setup(repo => repo.Update(professor))
            .ReturnsAsync(professor);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        _professorRepositoryMock.Verify(repo => repo.Update(professor), Times.Once);
    }


}
