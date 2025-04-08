using InterRapidisimoEventBus.Abstractions;

namespace InterRapidisimoEventBus.Events;

public class SubjectAssignedToProfessorEvent : IIntegrationEvent
{
    public Guid EventId { get; }
    public DateTime CreationDate { get; }
    public Guid SubjectId { get; set; }
    public Guid ProfessorId { get; set; }

    public SubjectAssignedToProfessorEvent(Guid subjectId, Guid professorId)
    {
        EventId = Guid.NewGuid();
        CreationDate = DateTime.UtcNow;
        SubjectId = subjectId;
        ProfessorId = professorId;
    }
}
