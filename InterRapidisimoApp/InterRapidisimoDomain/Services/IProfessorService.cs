using CSharpFunctionalExtensions;
using InterRapidisimoDomain.Entities;

namespace InterRapidisimoDomain.Services;

public interface IProfessorService
{
    Task<Result<Professor>> CreateProfessor(string name, string surname, string email);
    Task<Result> AssignSubjectToProfessor(Guid professorId, Guid subjectId);
}
