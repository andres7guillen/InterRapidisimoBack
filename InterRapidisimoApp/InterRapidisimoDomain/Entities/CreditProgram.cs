using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterRapidisimoDomain.Entities
{
    public class CreditProgram
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        private readonly List<StudentCreditProgram> _studentCreditPrograms = new();
        public IReadOnlyCollection<StudentCreditProgram> StudentCreditPrograms => _studentCreditPrograms.AsReadOnly();
    }
}
