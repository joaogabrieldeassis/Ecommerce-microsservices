using EShop.Shared.EventBus.Abstraction;
using EShop.Shared.EventBus.Events;

namespace EShop.Shared.EventBus.Interfaces;

public interface IMessageBus 
{
    Task PublishAsync(IntegrationEvent @event);

    Task SubscribeAsync<T, TH>() where T : IntegrationEvent  where TH : IIntegrationEventHandler<T>; 
}