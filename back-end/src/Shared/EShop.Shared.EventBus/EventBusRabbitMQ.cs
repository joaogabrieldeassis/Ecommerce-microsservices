using EShop.Shared.EventBus.Abstraction;
using EShop.Shared.EventBus.Events;
using EShop.Shared.EventBus.Interfaces;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace EShop.Shared.EventBus;

public class EventBusRabbitMQ(IEventBusSubscriptionsManager subsManager, string queueName) : IMessageBus
{
    private readonly IEventBusSubscriptionsManager _subsManager = subsManager;
    private string _queueName = queueName;
    const string BROKER_NAME = "eshop_event_bus";

    public async Task Publish(IntegrationEvent @event)
    {
        var eventName = @event.GetType().Name;
        var factory = new ConnectionFactory() { HostName = "localhost" };
        using var connection = await factory.CreateConnectionAsync();
        using var channel = await connection.CreateChannelAsync();

        await channel.ExchangeDeclareAsync(exchange: BROKER_NAME, type: "direct");

        string message = JsonConvert.SerializeObject(@event);
        var body = Encoding.UTF8.GetBytes(message);
        await channel.BasicPublishAsync(exchange: BROKER_NAME, routingKey: eventName, body: body);
    }

    public async Task Subscribe<T, TH>() where T : IntegrationEvent where TH : IIntegrationEventHandler<T>
    {
        var eventName = _subsManager.GetEventKey<T>();
        var containsKey = _subsManager.HasSubscriptionsForEvent(eventName);
        if (!containsKey)
        {
            var factory = new ConnectionFactory { HostName = "localhost" };
            using var connection = await factory.CreateConnectionAsync();
            using var channel = await connection.CreateChannelAsync();
            await channel.QueueBindAsync(queue: _queueName, exchange: BROKER_NAME, routingKey: eventName);
        }
        _subsManager.AddSubscription<T, TH>();
    }
}