using CSharpFunctionalExtensions;

namespace InterRapidisimoDomain.Entities;

public class Professor
{
    private const int MaxSubjects = 2;
    private readonly List<ProfessorSubject> _professorSubjects = new();

    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Name { get; private set; } = string.Empty;
    public string SurName { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;

    public IReadOnlyCollection<ProfessorSubject> ProfessorSubjects => _professorSubjects.AsReadOnly();
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
            return Result.Failure<Professor>("El nombre y apellido son obligatorios.");
        if (!email.Contains("@"))
            return Result.Failure<Professor>("Email inválido.");

        return Result.Success(new Professor
        {
            Name = name,
            SurName = surname,
            Email = email
        });
    }

    public Result AssignSubject(Subject subject)
    {
        if (_professorSubjects.Count >= MaxSubjects)
            return Result.Failure("Un profesor no puede dictar más de 2 materias.");

        _professorSubjects.Add(new ProfessorSubject(Id, subject.Id));
        return Result.Success();
    }

}
