namespace InterRapidisimoEventBus.Abstractions;
public interface IEventHandler<in TEvent> where TEvent : IIntegrationEvent
{
    Task Handle(TEvent @event);
}
