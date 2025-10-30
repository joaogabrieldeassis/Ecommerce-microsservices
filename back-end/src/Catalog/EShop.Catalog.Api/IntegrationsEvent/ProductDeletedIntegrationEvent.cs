using EShop.Shared.EventBus.Events;

namespace EShop.Catalog.Api.IntegrationsEvent;

public record ProductDeletedIntegrationEvent : IntegrationEvent
{
    public ProductDeletedIntegrationEvent(Guid productId)
    {
        ProductId = productId;
        
    }

    public Guid ProductId { get; } = Guid.Empty;
}