using CSharpFunctionalExtensions;
using InterRapidisimoDomain.Entities;

namespace InterRapidisimoDomain.Repositories;

public interface ISubjectRepository
{
    Task<Result<Subject>> Create(Subject subject);
    Task<Maybe<Subject>> GetById(Guid id);
    Task<Result<List<Subject>>> GetAll();
    Task<Result<List<Subject>>> GetSubjectsByProfessorId(Guid professorId);
    Task<Result<bool>> Delete(Guid id);
    Task<Maybe<List<Subject>>> GetSubjectsByStudentId(Guid studentId);
}
