using InterRapidisimoEventBus.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterRapidisimoEventBus.Events
{
    public class SubjectUpdatedEvent : IIntegrationEvent
    {
        public Guid EventId { get; }
        public DateTime CreationDate { get; }
        public Guid SubjectId { get; set; }
        public string Name { get; set; }

        public SubjectUpdatedEvent(Guid subjectId, string name)
        {
            EventId = Guid.NewGuid();
            CreationDate = DateTime.UtcNow;
            SubjectId = subjectId;
            Name = name;
        }
    }
}
