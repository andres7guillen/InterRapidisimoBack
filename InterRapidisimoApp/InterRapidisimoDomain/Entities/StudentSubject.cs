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
        Student = student;
        StudentId = student.Id;
        Subject = subject;
        SubjectId = subject.Id;
        Professor = professor;
        ProfessorId = professor.Id;
    }
}
