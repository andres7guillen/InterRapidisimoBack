using CSharpFunctionalExtensions;
using InterRapidisimoData.Context;
using InterRapidisimoDomain.Entities;
using InterRapidisimoDomain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace InterRapidisimoInfrastructure.Repositories;

public class CreditProgramRepository : ICreditProgramRepository
{
    private readonly AppDbContext _context;

    public CreditProgramRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Result<CreditProgram>> Create(CreditProgram creditProgram)
    {
        await _context.CreditPrograms.AddAsync(creditProgram);
        await _context.SaveChangesAsync();
        return Result.Success(creditProgram);
    }

    public async Task<Result<bool>> Delete(Guid id)
    {
        var creditPrograms = await _context.CreditPrograms.FindAsync(id);
        if (creditPrograms is null)
            return Result.Failure<bool>("Credit program not found");
        _context.CreditPrograms.Remove(creditPrograms);
        return await _context.SaveChangesAsync() > 0;

    }

    public async Task<Result<List<CreditProgram>>> GetAll()
    {
        return await _context.CreditPrograms
            .ToListAsync();
    }

    public async Task<Maybe<CreditProgram>> GetByIdAsync(Guid creditProgramId)
    {
        var creditProgram = await _context.CreditPrograms.FindAsync(creditProgramId);
        return creditProgram == null
            ? Maybe.None
            : Maybe.From(creditProgram);
    }


    public async Task<Result<CreditProgram>> Update(CreditProgram creditProgram)
    {
        _context.CreditPrograms.Update(creditProgram);
        return await _context.SaveChangesAsync() > 0
        ? Result.Success(creditProgram)
        : Result.Failure<CreditProgram>("Error updating credit program");
    }
}
