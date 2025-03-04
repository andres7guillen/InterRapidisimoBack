using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterRapidisimoDomain.DTOs;

public class ClassmateDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public string SubjectName { get; set; } = string.Empty;

    public ClassmateDto(Guid id, string name, string surname, string subjectName)
    {
        Id = id;
        Name = name;
        Surname = surname;
        SubjectName = subjectName;
    }
}
