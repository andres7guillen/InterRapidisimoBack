using CSharpFunctionalExtensions;
using InterRapidisimoData.Context;
using InterRapidisimoDomain.Entities;
using InterRapidisimoDomain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace InterRapidisimoInfrastructure.Repositories;

public class ProfessorRepository : IProfessorRepository
{
    private readonly AppDbContext _context;

    public ProfessorRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Result<Professor>> CreateProfessor(Professor professor)
    {
        await _context.Professors.AddAsync(professor);
        await _context.SaveChangesAsync();
        return Result.Success(professor);
    }

    public async Task<Maybe<Professor>> GetById(Guid id)
    {
        var professor = await _context.Professors
            .Include(p => p.ProfessorSubjects)
            .FirstOrDefaultAsync(p => p.Id == id);
        return professor == null
            ? Maybe.None
            : Maybe.From(professor);
    }

    public async Task<Result<List<Professor>>> GetAll()
    {
        var list = await _context.Professors
            .Include(p => p.ProfessorSubjects)
            .ToListAsync();
        return list.Count > 1
            ? Result.Success(list)
            : Result.Failure<List<Professor>>("There's no professors created");
    }

    public async Task<Result<Professor>> Update(Professor professor)
    {
        _context.Professors.Update(professor);
        await _context.SaveChangesAsync();
        return professor;
    }
}
