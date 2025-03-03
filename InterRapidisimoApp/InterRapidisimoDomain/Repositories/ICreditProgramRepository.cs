using CSharpFunctionalExtensions;
using InterRapidisimoDomain.Entities;

namespace InterRapidisimoDomain.Repositories;

public interface ICreditProgramRepository
{
    Task<Result<CreditProgram>> Create(CreditProgram creditProgram);
    Task<Maybe<CreditProgram>> GetByIdAsync(Guid creditProgramId);
    Task<Result<List<CreditProgram>>> GetAll();
    Task<Result<CreditProgram>> Update(CreditProgram creditProgram);
    Task<Result<bool>> Delete(Guid id);

}
