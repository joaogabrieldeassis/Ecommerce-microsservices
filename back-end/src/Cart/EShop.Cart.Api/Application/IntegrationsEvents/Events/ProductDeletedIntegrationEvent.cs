using EShop.Shared.EventBus.Events;

namespace EShop.Cart.Api.Application.IntegrationsEvents.Events;

public record ProductDeletedIntegrationEvent : IntegrationEvent
{
    public ProductDeletedIntegrationEvent(Guid productId)
    {
        ProductId = productId;

    }

    public Guid ProductId { get; } = Guid.Empty;
}