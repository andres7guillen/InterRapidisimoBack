namespace InterRapidisimoEventBus.Abstractions;

public interface IEventBus
{
    Task Publish<TEvent>(TEvent @event) where TEvent : IIntegrationEvent;
    void Subscribe<TEvent, THandler>()
        where TEvent : IIntegrationEvent
        where THandler : IEventHandler<TEvent>;

    void Unsubscribe<TEvent, THandler>()
        where TEvent : IIntegrationEvent
        where THandler : IEventHandler<TEvent>;
}
