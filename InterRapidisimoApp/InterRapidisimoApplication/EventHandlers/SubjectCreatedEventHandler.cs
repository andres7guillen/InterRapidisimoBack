using InterRapidisimoEventBus.Abstractions;
using InterRapidisimoEventBus.Events;
using Microsoft.Extensions.Logging;

namespace InterRapidisimoApplication.EventHandlers;

public class SubjectCreatedEventHandler : IEventHandler<SubjectCreatedEvent>
{
    private readonly ILogger<SubjectCreatedEventHandler> _logger;

    public SubjectCreatedEventHandler(ILogger<SubjectCreatedEventHandler> logger)
    {
        _logger = logger;
    }

    public async Task Handle(SubjectCreatedEvent @event)
    {
        _logger.LogInformation($"Se ha creado una nueva materia con ID: {@event.SubjectId} y Nombre: {@event.Name}");
        // Aquí tu lógica de negocio para reaccionar al evento
        await Task.CompletedTask;
    }
}
