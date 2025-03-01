using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterRapidisimoDomain.Entities
{
    public class Subject
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public int Credits { get; set; } = 3;

        public ICollection<ProfessorSubject> ProfessorSubjects { get; set; } = new List<ProfessorSubject>();
        public ICollection<StudentSubject> StudentSubjects { get; set; } = new List<StudentSubject>();
    }
}
