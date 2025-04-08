using AutoMapper;
using CSharpFunctionalExtensions;
using InterRapidisimoApplication.Commands;
using InterRapidisimoDomain.DTOs;
using InterRapidisimoDomain.Entities;
using InterRapidisimoDomain.Repositories;
using Moq;

namespace InterRapidisimoTests.Commands;

public class CreateProfessorCommandHandlerTests
{
    private readonly Mock<IProfessorRepository> _mockProfessorRepository;
    private readonly Mock<IMapper> _mockMapper;
    private readonly CreateProfessorCommand.CreateProfessorCommandHandler _handler;

    public CreateProfessorCommandHandlerTests()
    {
        _mockProfessorRepository = new Mock<IProfessorRepository>();
        _mockMapper = new Mock<IMapper>();
        _handler = new CreateProfessorCommand.CreateProfessorCommandHandler(_mockProfessorRepository.Object, _mockMapper.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenProfessorCreationFails()
    {
        // Arrange
        var command = new CreateProfessorCommand("John", "Doe", "invalid-email");
        var failureResult = Result.Failure<Professor>("Invalid email format");
        var professor = Professor.Create("Andres", "Guillen", "andres7guillen@gmail.com");

        // Simula que `Professor.Create` retorna un fallo
        _mockProfessorRepository
            .Setup(r => r.CreateProfessor(It.IsAny<Professor>()))
            .ReturnsAsync(professor.Value);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Equal("Invalid email format", result.Error);

        _mockProfessorRepository.Verify(r => r.CreateProfessor(It.IsAny<Professor>()), Times.Never);
        _mockMapper.Verify(m => m.Map<ProfessorDto>(It.IsAny<Professor>()), Times.Never);
    }

}
