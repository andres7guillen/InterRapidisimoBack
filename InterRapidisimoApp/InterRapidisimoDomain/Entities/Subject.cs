using CSharpFunctionalExtensions;

namespace InterRapidisimoDomain.Entities;

public class Subject
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Name { get; private set; } = string.Empty;
    public int Credits { get; private set; } = 3;

    public List<ProfessorSubject> ProfessorSubjects { get; set; } = new();
    public List<StudentSubject> StudentSubjects { get; private set; } = new();

    private Subject() { }

    private Subject(string name)
    {
        Name = name;
    }

    public static Result<Subject> CreateSubject(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Result.Failure<Subject>("The subject's name is required");

        return Result.Success(new Subject(name));
    }
}
