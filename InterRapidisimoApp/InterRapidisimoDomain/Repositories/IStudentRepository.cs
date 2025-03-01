using CSharpFunctionalExtensions;
using InterRapidisimoDomain.Entities;

namespace InterRapidisimoDomain.Repositories;

public interface IStudentRepository
{
    Task<Result<IEnumerable<Student>>> GetAllAsync();
    Task<Maybe<Student>> GetByIdAsync(Guid studentId);
    Task<Result<IEnumerable<Student>>> GetClassmatesAsync(IEnumerable<Guid> subjectIds, Guid studentId);
    Task<Maybe<Student>> GetByIdWithSubjectsAsync(Guid studentId);
}
