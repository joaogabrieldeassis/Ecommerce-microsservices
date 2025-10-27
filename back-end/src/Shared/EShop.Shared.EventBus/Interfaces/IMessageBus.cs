using EShop.Shared.EventBus.Abstraction;
using EShop.Shared.EventBus.Events;

namespace EShop.Shared.EventBus.Interfaces;

public interface IMessageBus 
{
    Task Publish(IntegrationEvent @event);

    Task Subscribe<T, TH>() where T : IntegrationEvent  where TH : IIntegrationEventHandler<T>; 
}