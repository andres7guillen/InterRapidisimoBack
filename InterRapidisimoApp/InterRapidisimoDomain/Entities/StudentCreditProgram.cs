using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterRapidisimoDomain.Entities
{
    public class StudentCreditProgram
    {
        public Guid StudentId { get; set; }
        public Student Student { get; set; } = null!;

        public Guid CreditProgramId { get; set; }
        public CreditProgram CreditProgram { get; set; } = null!;
    }
}
