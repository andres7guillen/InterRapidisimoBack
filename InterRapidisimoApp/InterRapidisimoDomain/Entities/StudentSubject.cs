namespace InterRapidisimoDomain.Entities;

public class StudentSubject
{
    public Guid StudentId { get; private set; }
    public Student Student { get; private set; } = null!;
    public Guid SubjectId { get; private set; }
    public Subject Subject { get; private set; } = null!;
    public Guid ProfessorId { get; private set; }
    public Professor Professor { get; private set; } = null!;

    private StudentSubject() { }

    public StudentSubject(Student student, Subject subject, Professor professor)
    {
        // Validaciones de negocio
        if (student == null) throw new ArgumentNullException(nameof(student));
        if (subject == null) throw new ArgumentNullException(nameof(subject));
        if (professor == null) throw new ArgumentNullException(nameof(professor));

        // Regla 5: Un estudiante solo puede tomar 3 materias.
        if (student.StudentSubjects.Count >= 3)
            throw new InvalidOperationException("El estudiante no puede inscribirse en más de 3 materias.");

        // Regla 7: El estudiante no puede tener clases con el mismo profesor.
        if (student.StudentSubjects.Any(ss => ss.ProfessorId == professor.Id))
            throw new InvalidOperationException("El estudiante ya tiene una materia con este profesor.");

        Student = student;
        StudentId = student.Id;
        Subject = subject;
        SubjectId = subject.Id;
        Professor = professor;
        ProfessorId = professor.Id;
    }
}


