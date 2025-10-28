using EShop.Shared.EventBus.Abstraction;
using EShop.Shared.EventBus.Events;
using EShop.Shared.EventBus.Interfaces;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace EShop.Shared.EventBus;

public class EventBusRabbitMQ(IEventBusSubscriptionsManager subsManager, string queueName, IServiceProvider serviceProvider) : IMessageBus
{
    private readonly IEventBusSubscriptionsManager _subsManager = subsManager;
    private readonly string _queueName = queueName;
    private readonly IServiceProvider _serviceProvider = serviceProvider;
    const string BROKER_NAME = "eshop_event_bus";
    private readonly ConnectionFactory _factory = new() { HostName = "localhost" };

    // Publish permanece semelhante
    public async Task PublishAsync(IntegrationEvent @event)
    {
        var eventName = @event.GetType().Name;
        using var connection = await _factory.CreateConnectionAsync();
        using var channel = await connection.CreateChannelAsync();

        await channel.ExchangeDeclareAsync(exchange: BROKER_NAME, type: "direct", durable: true);

        string message = JsonConvert.SerializeObject(@event);
        var body = Encoding.UTF8.GetBytes(message);
        await channel.BasicPublishAsync(exchange: BROKER_NAME, routingKey: eventName, body: body);
    }

    public async Task SubscribeAsync<T, TH>() where T : IntegrationEvent where TH : IIntegrationEventHandler<T>
    {
        var eventName = _subsManager.GetEventKey<T>();
        var containsKey = _subsManager.HasSubscriptionsForEvent(eventName);
        if (!containsKey)
        {
            using var connection = await _factory.CreateConnectionAsync();
            using var channel = await connection.CreateChannelAsync();

            await channel.ExchangeDeclareAsync(exchange: BROKER_NAME, type: "direct", durable: true);

            await channel.QueueDeclareAsync(queue: _queueName,
                                           durable: true,
                                           exclusive: false,
                                           autoDelete: false,
                                           arguments: null);

            await channel.QueueBindAsync(queue: _queueName, exchange: BROKER_NAME, routingKey: eventName);
        }

        _subsManager.AddSubscription<T, TH>();

        await StartBasicConsume<T, TH>();
    }

    private async Task StartBasicConsume<T, TH>() where T : IntegrationEvent where TH : IIntegrationEventHandler<T>
    {
        var conn = await _factory.CreateConnectionAsync();
        var ch = await conn.CreateChannelAsync();
        var consumer = new AsyncEventingBasicConsumer(ch);

        consumer.ReceivedAsync += async (sender, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            T integrationEvent;
            try
            {
                integrationEvent = JsonConvert.DeserializeObject<T>(message)!;
            }
            catch
            {
                await ch.BasicNackAsync(ea.DeliveryTag, false, false);
                return;
            }

            // resolver e executar handler via scope
            try
            {
                using var scope = _serviceProvider.CreateScope();
                IIntegrationEventHandler<T>? handler = scope.ServiceProvider.GetService(typeof(TH)) as IIntegrationEventHandler<T>;
                if (handler != null)
                {
                    await handler.Handle(integrationEvent);
                }
                await ch.BasicAckAsync(ea.DeliveryTag, false);
            }
            catch
            {
                await ch.BasicAckAsync(ea.DeliveryTag, false); // requeue on failure
            }
        };

        await ch.BasicConsumeAsync(queue: _queueName, autoAck: false, consumer: consumer);
    }
}