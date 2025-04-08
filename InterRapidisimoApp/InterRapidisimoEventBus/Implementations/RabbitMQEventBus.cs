using InterRapidisimoEventBus.Abstractions;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace InterRapidisimoEventBus.Implementations
{
    public class RabbitMQEventBus : IEventBus, IDisposable
    {
        private readonly RabbitMQConnection _persistentConnection;
        private readonly ILogger<RabbitMQEventBus> _logger;
        private readonly IServiceProvider _serviceProvider;
        private readonly string _exchangeName = "interrapidisimo_event_bus";
        private readonly string _queueName;
        private IModel _consumerChannel;

        private readonly ConcurrentDictionary<string, List<Type>> _handlers = new ConcurrentDictionary<string, List<Type>>();
        private readonly ConcurrentDictionary<string, List<Type>> _eventTypes = new ConcurrentDictionary<string, List<Type>>();

        public RabbitMQEventBus(RabbitMQConnection persistentConnection, ILogger<RabbitMQEventBus> logger, IServiceProvider serviceProvider, string queueName = null)
        {
            _persistentConnection = persistentConnection ?? throw new ArgumentNullException(nameof(persistentConnection));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
            _queueName = queueName ?? "interrapidisimo_default_queue";

            if (!_persistentConnection.IsConnected)
            {
                _persistentConnection.TryConnect();
            }

            _consumerChannel = CreateConsumerChannel();
            StartBasicConsume();
        }

        public async Task Publish<TEvent>(TEvent @event) where TEvent : IIntegrationEvent
        {
            if (!_persistentConnection.IsConnected)
            {
                _persistentConnection.TryConnect();
            }

            using (var channel = _persistentConnection.CreateModel())
            {
                var eventName = @event.GetType().Name;

                _logger.LogTrace("Publicando evento en RabbitMQ: {EventId} -> {Event}", @event.EventId, eventName);

                channel.ExchangeDeclare(exchange: _exchangeName, type: ExchangeType.Direct);

                var message = JsonConvert.SerializeObject(@event);
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(
                    exchange: _exchangeName,
                    routingKey: eventName,
                    basicProperties: null,
                    body: body);

                // No hay operaciones asíncronas bloqueantes aquí,
                // pero el método de la interfaz es Task, así que devolvemos Task.CompletedTask
                await Task.CompletedTask;
            }
        }

        public void Subscribe<TEvent, THandler>()
            where TEvent : IIntegrationEvent
            where THandler : IEventHandler<TEvent>
        {
            var eventName = typeof(TEvent).Name;
            var handlerType = typeof(THandler);

            if (!_eventTypes.ContainsKey(eventName))
            {
                _eventTypes.TryAdd(eventName, new List<Type>());
            }

            if (!_handlers.ContainsKey(eventName))
            {
                _handlers.TryAdd(eventName, new List<Type>());
            }

            if (_eventTypes[eventName].Any(ht => ht.GetType() == typeof(TEvent)))
            {
                throw new ArgumentException($"Ya existe una suscripción para el evento {eventName}.", nameof(TEvent));
            }

            if (_handlers[eventName].Any(ht => ht.GetType() == handlerType))
            {
                throw new ArgumentException($"El manejador {handlerType.Name} ya está registrado para el evento {eventName}.", nameof(THandler));
            }

            _eventTypes[eventName].Add(typeof(TEvent));
            _handlers[eventName].Add(handlerType);
            StartBasicConsume(); // Considerar si reiniciar el consumo es siempre necesario aquí
        }

        public void Unsubscribe<TEvent, THandler>()
            where TEvent : IIntegrationEvent
            where THandler : IEventHandler<TEvent>
        {
            var eventName = typeof(TEvent).Name;
            var handlerType = typeof(THandler);

            if (_handlers.ContainsKey(eventName) && _handlers[eventName].Contains(handlerType))
            {
                _handlers[eventName].Remove(handlerType);
                if (!_handlers[eventName].Any())
                {
                    _handlers.TryRemove(eventName, out _);
                }
                _eventTypes[eventName].Remove(typeof(TEvent));
                if (!_eventTypes[eventName].Any())
                {
                    _eventTypes.TryRemove(eventName, out _);
                }
                // No es necesario reiniciar el consumo aquí, ya que los manejadores se invocan en memoria.
            }
        }

        private IModel CreateConsumerChannel()
        {
            if (!_persistentConnection.IsConnected)
            {
                _persistentConnection.TryConnect();
            }

            var channel = _persistentConnection.CreateModel();

            channel.ExchangeDeclare(exchange: _exchangeName, type: ExchangeType.Direct);

            channel.QueueDeclare(queue: _queueName,
                                 durable: true,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            channel.CallbackException += (sender, ea) =>
            {
                _logger.LogWarning(ea.Exception, "Recreando el canal de consumidor de RabbitMQ");
                _consumerChannel.Dispose();
                _consumerChannel = CreateConsumerChannel();
                StartBasicConsume();
            };

            return channel;
        }

        private void StartBasicConsume()
        {
            if (_consumerChannel == null)
            {
                _logger.LogError("No se puede iniciar el consumo básico porque el canal del consumidor no está inicializado.");
                return;
            }

            var consumer = new AsyncEventingBasicConsumer(_consumerChannel);

            consumer.Received += async (model, ea) =>
            {
                var eventName = ea.RoutingKey;
                var message = Encoding.UTF8.GetString(ea.Body.ToArray());

                try
                {
                    if (_handlers.ContainsKey(eventName))
                    {
                        using (var scope = _serviceProvider.CreateScope())
                        {
                            var handlers = _handlers[eventName];
                            foreach (var handlerType in handlers)
                            {
                                var handler = scope.ServiceProvider.GetService(handlerType);
                                if (handler == null) continue;

                                var eventType = _eventTypes[eventName].SingleOrDefault(t => t.Name == eventName);
                                var integrationEvent = JsonConvert.DeserializeObject(message, eventType);
                                var concreteType = typeof(IEventHandler<>).MakeGenericType(eventType);

                                await (Task)concreteType.GetMethod("Handle").Invoke(handler, new object[] { integrationEvent });
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error al procesar el evento de RabbitMQ: {EventName}", eventName);
                }

                _consumerChannel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
            };

            _consumerChannel.BasicConsume(
                queue: _queueName,
                autoAck: false,
                consumer: consumer);
        }

        public void Dispose()
        {
            if (_consumerChannel != null)
            {
                _consumerChannel.Dispose();
            }
        }
    }
}
