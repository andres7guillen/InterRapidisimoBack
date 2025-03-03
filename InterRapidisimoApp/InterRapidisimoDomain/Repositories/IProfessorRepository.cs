using CSharpFunctionalExtensions;
using InterRapidisimoDomain.Entities;

namespace InterRapidisimoDomain.Repositories;

public interface IProfessorRepository
{
    Task<Result<Professor>> CreateProfessor(Professor professor);
    Task<Maybe<Professor>>? GetById(Guid id);
    Task<Result<List<Professor>>> GetAll();
    Task<Result<Professor>> Update(Professor professor);
    Task<Result<bool>> Delete(Professor professor);
    Task<Result<int>> CountWithTwoSubjectsAsync();
}
