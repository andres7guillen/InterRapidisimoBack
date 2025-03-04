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

    public async Task<Result<Student>> Create(Student student)
    {
        await _context.Students.AddAsync(student);
        await _context.SaveChangesAsync();
        return Result.Success(student);
    }

    public async Task<Result<bool>> Delete(Guid id)
    {
        var student = await _context.Students.FindAsync(id);
        if (student == null)
            return Result.Failure<bool>("Error removing student");
        _context.Students.Remove(student);
        return await _context.SaveChangesAsync() > 0
            ? Result.Success(true)
            : Result.Failure<bool>("Error removing subject");
    }

    public async Task<Result<IEnumerable<Student>>> GetAllAsync()
    {
        return await _context.Students
            .Include(s => s.StudentSubjects)
            .ThenInclude(sc => sc.Subject)
            .ToListAsync();
    }

    public async Task<Maybe<Student>> GetByIdAsync(Guid studentId)
    {
        return await _context.Students
            .Include(s => s.StudentSubjects)
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
        var classmates = await _context.Students
        .Where(s => s.StudentSubjects.Any(ss => subjectIds.Contains(ss.SubjectId)) && s.Id != studentId)
        .ToListAsync();

        return classmates.Any()
            ? Result.Success<IEnumerable<Student>>(classmates)
            : Result.Failure<IEnumerable<Student>>("No classmates found.");
    }

    public async Task<Result<List<Student>>> GetClassmatesByStudentIdAsync(Guid studentId)
    {
        var student = await _context.Students
        .Include(s => s.StudentCreditPrograms) // Si hay relación con programas de créditos
        .FirstOrDefaultAsync(s => s.Id == studentId);

        if (student == null)
            return new List<Student>();

        var subjectIds = await _context.StudentSubjects
            .Where(ss => ss.StudentId == studentId)
            .Select(ss => ss.SubjectId)
            .ToListAsync();

        if (!subjectIds.Any())
            return new List<Student>();

        return await _context.Students
            .Where(s => s.Id != studentId && _context.StudentSubjects
                .Any(ss => ss.StudentId == s.Id && subjectIds.Contains(ss.SubjectId)))
            .ToListAsync();
    }

    public async Task<Result<List<Student>>> GetStudentsBySubjectIdAsync(Guid subjectId)
    {
        var students = await _context.Students
            .Where(s => s.StudentSubjects.Any(ss => ss.SubjectId == subjectId))
            .ToListAsync();

        return students.Count >= 1
            ? Result.Success(students)
            : Result.Failure<List<Student>>("There are no students enrolled in this subject.");
    }

    public async Task<Result<Student>> GetStudentWithSubjectsAsync(Guid studentId)
    {
        var student = await _context.Students
            .Include(s => s.StudentSubjects)
            .FirstOrDefaultAsync(s => s.Id == studentId);

        return student is null
            ? Result.Failure<Student>("Estudiante no encontrado.")
            : Result.Success(student);
    }

    public async Task<Result<bool>> UpdateStudent(Student student)
    {
        _context.Students.Update(student);
        return await _context.SaveChangesAsync() > 0;
    }
}
