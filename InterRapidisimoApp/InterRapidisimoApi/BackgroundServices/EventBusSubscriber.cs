using InterRapidisimoApplication.EventHandlers;
using InterRapidisimoEventBus.Abstractions;
using InterRapidisimoEventBus.Events;

namespace InterRapidisimoApi.BackgroundServices
{
    public class EventBusSubscriber : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;

        public EventBusSubscriber(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var eventBus = scope.ServiceProvider.GetRequiredService<IEventBus>();
                eventBus.Subscribe<SubjectCreatedEvent, SubjectCreatedEventHandler>();
                // Suscribe aquí otros manejadores y eventos
            }

            return;
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var eventBus = scope.ServiceProvider.GetRequiredService<IEventBus>();
                // Aquí podrías realizar la cancelación de suscripciones si es necesario
                // eventBus.Unsubscribe<SubjectCreatedEvent, SubjectCreatedEventHandler>();
            }

            return;
        }
    }
}
