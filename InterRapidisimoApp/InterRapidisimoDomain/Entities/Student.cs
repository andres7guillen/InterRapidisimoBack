using CSharpFunctionalExtensions;

namespace InterRapidisimoDomain.Entities;

public class Student
{
    private readonly List<Subject> _subjects = new();
    private readonly List<StudentSubject> _studentSubjects = new();
    private readonly List<StudentCreditProgram> _creditPrograms = new();

    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Name { get; private set; }
    public string SurName { get; private set; }
    public string Email { get; private set; }

    public IReadOnlyCollection<StudentSubject> StudentSubjects => _studentSubjects.AsReadOnly();
    public IReadOnlyCollection<Subject> EnrolledSubjects => _subjects.AsReadOnly();
    private readonly List<StudentCreditProgram> _studentCreditPrograms = new();
    public IReadOnlyCollection<StudentCreditProgram> StudentCreditPrograms => _studentCreditPrograms.AsReadOnly();

    private Student(string name, string surname, string email)
    {
        Name = name;
        SurName = surname;
        Email = email;
    }

    private Student() { }

    public static Result<Student> CreateStudent(string name, string surname, string email)
    {
        if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(surname))
            return Result.Failure<Student>("Name and surname are required.");
        if (!email.Contains("@"))
            return Result.Failure<Student>("Invalid email.");

        return Result.Success(new Student(name, surname, email));
    }
    public Result AddStudentSubject(StudentSubject studentSubject)
    {
        _studentSubjects.Add(studentSubject);
        return Result.Success();
    }
    public Result EnrollInCreditProgram(StudentCreditProgram program)
    {
        if (program == null)
            return Result.Failure("Student credit program is invalid.");

        if (_creditPrograms.Any(p => p.CreditProgramId == program.CreditProgramId))
            return Result.Failure("You are already enrolled on this credit program.");

        _creditPrograms.Add(program);
        return Result.Success();
    }
}



