using CSharpFunctionalExtensions;
using InterRapidisimoData.Context;
using InterRapidisimoDomain.Entities;
using InterRapidisimoDomain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace InterRapidisimoInfrastructure.Repositories;

public class SubjectRepository : ISubjectRepository
{
    private readonly AppDbContext _context;

    public SubjectRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task<Result<Subject>> Create(Subject subject)
    {
        await _context.Subjects.AddAsync(subject);
        await _context.SaveChangesAsync();
        return Result.Success(subject);
    }

    public async Task<Result<bool>> Delete(Guid id)
    {
        var subject = await _context.Subjects.FindAsync(id);
        if(subject == null)
            return Result.Failure<bool>("Error removing subject");
        _context.Subjects.Remove(subject);
        return await _context.SaveChangesAsync() > 0
            ? Result.Success(true)
            : Result.Failure<bool>("Error removing subject");
    }

    public async Task<Result<List<Subject>>> GetAll()
    {
        var list = await _context.Subjects.ToListAsync();
        return list.Count > 1
            ? Result.Success(list)
            : Result.Failure<List<Subject>>("There's no professors created");
    }

    public async Task<Maybe<Subject>> GetById(Guid id)
    {
        var subject = await _context.Subjects
        .Include(s => s.ProfessorSubjects) 
        .ThenInclude(ps => ps.Professor)   
        .FirstOrDefaultAsync(s => s.Id == id);

        return subject == null ? Maybe.None : Maybe.From(subject);
    }

    public async Task<Result<List<Subject>>> GetSubjectsByProfessorId(Guid professorId)
    {
        var list = await _context.Subjects
            .Where(s => s.ProfessorSubjects.Any(ps => ps.ProfessorId == professorId))
            .ToListAsync();
        return list.Count >= 1
            ? Result.Success(list)
            : Result.Failure<List<Subject>>("There are not subjects that professor teaches");
    }

    public async Task<Maybe<List<Subject>>> GetSubjectsByStudentId(Guid studentId)
    {
        var subjects = await _context.StudentSubjects
        .Where(ss => ss.StudentId == studentId)
        .Select(ss => ss.Subject)
        .ToListAsync();

        return subjects.Any() ? Maybe<List<Subject>>.From(subjects) : Maybe<List<Subject>>.None;
    }
}
