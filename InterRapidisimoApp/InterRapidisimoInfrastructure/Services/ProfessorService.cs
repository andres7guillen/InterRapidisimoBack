using CSharpFunctionalExtensions;
using InterRapidisimoDomain.Entities;
using InterRapidisimoDomain.Repositories;
using InterRapidisimoDomain.Services;

namespace InterRapidisimoInfrastructure.Services;

public class ProfessorService : IProfessorService
{
    private readonly IProfessorRepository _professorRepository;

    public ProfessorService(IProfessorRepository professorRepository)
    {
        _professorRepository = professorRepository;
    }
    public async Task<Result<Professor>> CreateProfessor(string name, string surname, string email)
    {
        var professor = Professor.Create(name, surname, email);
        if (professor.IsFailure)
            return Result.Failure<Professor>(professor.Error);

        await _professorRepository.CreateProfessor(professor.Value);
        return Result.Success(professor.Value);
    }
}
