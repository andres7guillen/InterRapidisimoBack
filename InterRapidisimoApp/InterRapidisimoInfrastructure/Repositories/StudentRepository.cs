using CSharpFunctionalExtensions;
using InterRapidisimoData.Context;
using InterRapidisimoDomain.Entities;
using InterRapidisimoDomain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace InterRapidisimoInfrastructure.Repositories;

public class StudentRepository : IStudentRepository
{
    private readonly AppDbContext _context;

    public StudentRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Result<IEnumerable<Student>>> GetAllAsync()
    {
        return await _context.Students
            .Include(s => s.StudentCourses)
            .ThenInclude(sc => sc.Subject)
            .ToListAsync();
    }

    public async Task<Maybe<Student>> GetByIdAsync(Guid studentId)
    {
        return await _context.Students
            .Include(s => s.StudentCourses)
            .ThenInclude(sc => sc.Subject)
            .FirstOrDefaultAsync(s => s.Id == studentId);
    }

    public async Task<Maybe<Student>> GetByIdWithSubjectsAsync(Guid studentId)
    {
        var student = await _context.Students
            .Include(s => s.StudentSubjects)
                .ThenInclude(ss => ss.Subject) // Cargar las materias inscritas
            .FirstOrDefaultAsync(s => s.Id == studentId);
        return student == null
            ? Maybe.None
            : Maybe.From(student);
    }

    public async Task<Result<IEnumerable<Student>>> GetClassmatesAsync(IEnumerable<Guid> subjectIds, Guid studentId)
    {
        return await _context.Students
            .Where(s => s.Id != studentId && s.StudentCourses.Any(sc => subjectIds.Contains(sc.SubjectId)))
            .ToListAsync();
    }
}
