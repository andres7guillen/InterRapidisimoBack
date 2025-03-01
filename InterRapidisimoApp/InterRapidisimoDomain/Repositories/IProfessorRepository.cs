using CSharpFunctionalExtensions;
using InterRapidisimoDomain.Entities;

namespace InterRapidisimoDomain.Repositories;

public interface IProfessorRepository
{
    Task<Result<Professor>> CreateProfessor(Professor professor);
    Task<Maybe<Professor>>? GetById(Guid id);
    Task<Result<List<Professor>>> GetAll();
    Task<Result<Professor>> Update(Professor professor);

}
