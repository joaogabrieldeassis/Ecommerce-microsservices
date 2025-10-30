using EShop.Shared.EventBus.Events;

namespace EShop.Cart.Api.Application.IntegrationsEvents.Events;

public record ProductUpdatedIntegrationEvent : IntegrationEvent
{
    public ProductUpdatedIntegrationEvent(Guid productId, string name, int quantityInStock, decimal price)
    {
        ProductId = productId;
        Name = name;
        QuantityInStock = quantityInStock;
        Price = price;
    }

    public Guid ProductId { get; } = Guid.Empty;
    public string Name { get; } = string.Empty;
    public int QuantityInStock { get; }
    public decimal Price { get; }
}