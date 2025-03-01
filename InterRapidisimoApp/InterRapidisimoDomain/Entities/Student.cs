using CSharpFunctionalExtensions;

namespace InterRapidisimoDomain.Entities
{
    public class Student
    {
        private const int MaxSubjects = 3;
        private readonly List<StudentSubject> _enrolledSubjects = new();
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; private set; } = string.Empty;
        public string SurName { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;


        public IReadOnlyCollection<StudentSubject> EnrolledCourses => _enrolledSubjects.AsReadOnly();
        public ICollection<StudentCreditProgram> StudentCreditPrograms { get; set; } = new List<StudentCreditProgram>();
        public ICollection<StudentSubject> StudentSubjects { get; set; } = new List<StudentSubject>();
        private readonly List<StudentSubject> _studentSubjects = new();
        public IReadOnlyCollection<StudentSubject> StudentCourses => _studentSubjects.AsReadOnly();

        private Student(string withName, string withSurname, string withEmail)
        {
            Name = withName;
            SurName  = withSurname;
            Email = withEmail;
        }

        private Student() { }

        public Result EnrollInSubject(Subject subject, Professor professor)
        {
            if (_enrolledSubjects.Count >= MaxSubjects)
                return Result.Failure("No puedes inscribirte en más de 3 materias.");

            if (_enrolledSubjects.Any(s => s.SubjectId == subject.Id))
                return Result.Failure("Ya estás inscrito en esta materia.");

            _enrolledSubjects.Add(new StudentSubject(this, subject, professor));
            return Result.Success();
        }
        public static Result<Student> CreateStudent(string name, string surname, string email)
        {
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(surname))
                return Result.Failure<Student>("Name and suername ae required.");
            if (!email.Contains("@"))
                return Result.Failure<Student>("Email invalid.");
            return Result.Success(new Student(withName: name, withSurname: surname, withEmail: email));
        }

        public List<Student> GetClassmates()
        {
            return _enrolledSubjects
                .SelectMany(ss => ss.Subject.StudentSubjects)  // Obtener todos los StudentSubject de la misma materia
                .Where(ss => ss.StudentId != this.Id)          // Excluir al estudiante actual
                .Select(ss => ss.Student)                      // Obtener los estudiantes
                .Distinct()
                .ToList();
        }

    }
}
