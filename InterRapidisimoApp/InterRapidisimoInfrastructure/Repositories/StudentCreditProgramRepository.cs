using CSharpFunctionalExtensions;
using InterRapidisimoData.Context;
using InterRapidisimoDomain.Entities;
using InterRapidisimoDomain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace InterRapidisimoInfrastructure.Repositories;

public class StudentCreditProgramRepository : IStudentCreditProgramRepository
{
    private readonly AppDbContext _context;

    public StudentCreditProgramRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Result<StudentCreditProgram>> CreateAsync(StudentCreditProgram studentCreditProgram)
    {
        try
        {
            await _context.StudentCreditPrograms.AddAsync(studentCreditProgram);
            await _context.SaveChangesAsync();
            return Result.Success(studentCreditProgram);
        }
        catch (Exception ex)
        {
            return Result.Failure<StudentCreditProgram>($"Error al inscribir al estudiante en el programa de créditos: {ex.Message}");
        }
    }

    public async Task<Maybe<StudentCreditProgram>> GetByIdAsync(Guid id)
    {
        var studentCreditProgram = await _context.StudentCreditPrograms
            .FindAsync(id);

        return studentCreditProgram == null
            ? Maybe.None
            : Maybe.From(studentCreditProgram);                
    }

    public async Task<Result<List<StudentCreditProgram>>> GetByStudentIdAsync(Guid studentId)
    {
        var studentCreditPrograms = await _context.StudentCreditPrograms
            .Where(scp => scp.StudentId == studentId)
            .ToListAsync();

        return studentCreditPrograms.Any()
            ? Result.Success(studentCreditPrograms)
            : Result.Failure<List<StudentCreditProgram>>("El estudiante no está inscrito en ningún programa de créditos.");
    }
}
