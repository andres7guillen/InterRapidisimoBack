using InterRapidisimoEventBus.Abstractions;

namespace InterRapidisimoEventBus.Events;

public class SubjectDeletedEvent : IIntegrationEvent
{
    public Guid EventId { get; }
    public DateTime CreationDate { get; }
    public Guid SubjectId { get; set; }

    public SubjectDeletedEvent(Guid subjectId)
    {
        EventId = Guid.NewGuid();
        CreationDate = DateTime.UtcNow;
        SubjectId = subjectId;
    }
}
