using InterRapidisimoEventBus.Abstractions;

namespace InterRapidisimoEventBus.Events;

public class SubjectCreatedEvent : IIntegrationEvent
{
    public Guid EventId { get; }
    public DateTime CreationDate { get; }
    public Guid SubjectId { get; set; }
    public string Name { get; set; }

    public SubjectCreatedEvent(Guid subjectId, string name)
    {
        EventId = Guid.NewGuid();
        CreationDate = DateTime.UtcNow;
        SubjectId = subjectId;
        Name = name;
    }
}
