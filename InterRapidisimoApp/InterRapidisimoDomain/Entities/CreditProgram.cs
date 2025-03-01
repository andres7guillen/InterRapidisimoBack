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

        public ICollection<StudentCreditProgram> StudentCreditPrograms { get; set; } = new List<StudentCreditProgram>();
    }
}
