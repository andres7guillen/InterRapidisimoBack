using CSharpFunctionalExtensions;

namespace InterRapidisimoDomain.Entities;

public class Professor
{
    private const int MaxSubjects = 2;
    private readonly List<ProfessorSubject> _professorSubjects = new();
    public List<ProfessorSubject> ProfessorSubjects { get; private set; } = new();

    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Name { get; private set; } = string.Empty;
    public string SurName { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;

    //public List<ProfessorSubject> ProfessorSubjects { get; set; } = new();
    public List<StudentSubject> StudentSubjects { get; private set; } = new();
    private Professor() { }
    private Professor(string name, string surname, string email)
    {
        Name = name;
        SurName = surname;
        Email = email;
    }

    public static Result<Professor> Create(string name, string surname, string email)
    {
        if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(surname))
            return Result.Failure<Professor>("Name and surname are required.");
        if (!email.Contains("@"))
            return Result.Failure<Professor>("Email invalid.");

        return Result.Success(new Professor
        {
            Name = name,
            SurName = surname,
            Email = email
        });
    }

    public Result AssignSubject(Subject subject)
    {
        if (ProfessorSubjects.Count >= MaxSubjects)
            return Result.Failure("Professor cannot teach more than 2 subjects");
        ProfessorSubjects.Add(new ProfessorSubject(Id, subject.Id));
        return Result.Success();
    }

}
