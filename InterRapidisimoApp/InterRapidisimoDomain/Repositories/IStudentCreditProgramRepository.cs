using CSharpFunctionalExtensions;
using InterRapidisimoDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterRapidisimoDomain.Repositories
{
    public interface IStudentCreditProgramRepository
    {
        Task<Result<StudentCreditProgram>> CreateAsync(StudentCreditProgram studentCreditProgram);
        Task<Maybe<StudentCreditProgram>> GetByIdAsync(Guid id);
        Task<Result<List<StudentCreditProgram>>> GetByStudentIdAsync(Guid studentId);
    }
}
