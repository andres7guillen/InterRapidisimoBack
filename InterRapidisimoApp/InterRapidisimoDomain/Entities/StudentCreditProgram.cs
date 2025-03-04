using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterRapidisimoDomain.Entities
{
    public class StudentCreditProgram
    {        
        public Guid StudentId { get; private set; }
        public Student Student { get; private set; } = null!;
        public Guid CreditProgramId { get; private set; }
        public CreditProgram CreditProgram { get; private set; } = null!;

        private StudentCreditProgram() { }

        private StudentCreditProgram(Guid studentId, Guid creditProgramId)
        {
            StudentId = studentId;
            CreditProgramId = creditProgramId;
        }
        public static Result<StudentCreditProgram> Create(Guid studentId, Guid creditProgramId)
        {
            if (studentId == Guid.Empty || creditProgramId == Guid.Empty)
                return Result.Failure<StudentCreditProgram>("IDs invalids.");

            return Result.Success(new StudentCreditProgram(studentId, creditProgramId));
        }

    }
}
