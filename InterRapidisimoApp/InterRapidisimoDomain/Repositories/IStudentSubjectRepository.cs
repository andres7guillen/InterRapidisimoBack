using CSharpFunctionalExtensions;
using InterRapidisimoDomain.Entities;

namespace InterRapidisimoDomain.Repositories;

public interface IStudentSubjectRepository
{
    Task<Result<StudentSubject>> CreateStudentSubjectAsync(StudentSubject studentSubject);
}
