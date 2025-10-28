using EShop.Shared.EventBus.Abstraction;
using EShop.Shared.EventBus.Events;

namespace EShop.Shared.EventBus.Interfaces;

public interface IEventBusSubscriptionsManager
{
    bool IsEmpty { get; }

    void AddSubscription<T, TH>()       where T : IntegrationEvent       where TH : IIntegrationEventHandler<T>;

    bool HasSubscriptionsForEvent(string eventName);
    void Clear();
    string GetEventKey<T>();
}