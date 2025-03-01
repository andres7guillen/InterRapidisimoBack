namespace InterRapidisimoDomain.Entities;

public class ProfessorSubject
{
    public Guid ProfessorId { get; private set; }
    public Professor Professor { get; private set; } = null!;
    public Guid SubjectId { get; private set; }
    public Subject Subject { get; private set; } = null!;

    private ProfessorSubject() { }

    public ProfessorSubject(Guid professorId, Guid subjectId)
    {
        ProfessorId = professorId;
        SubjectId = subjectId;
    }

}
