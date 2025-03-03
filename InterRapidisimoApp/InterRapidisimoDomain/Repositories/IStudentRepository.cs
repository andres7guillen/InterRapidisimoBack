using CSharpFunctionalExtensions;
using InterRapidisimoDomain.Entities;

namespace InterRapidisimoDomain.Repositories;

public interface IStudentRepository
{
    Task<Result<Student>> Create(Student student);
    Task<Result<IEnumerable<Student>>> GetAllAsync();
    Task<Maybe<Student>> GetByIdAsync(Guid studentId);
    Task<Result<IEnumerable<Student>>> GetClassmatesAsync(IEnumerable<Guid> subjectIds, Guid studentId);
    Task<Maybe<Student>> GetByIdWithSubjectsAsync(Guid studentId);
    Task<Result<List<Student>>> GetClassmatesByStudentIdAsync(Guid studentId);
    Task<Result<bool>> UpdateStudent(Student student);
    Task<Result<Student>> GetStudentWithSubjectsAsync(Guid studentId);
    Task<Result<List<Student>>> GetStudentsBySubjectIdAsync(Guid subjectId);
}
