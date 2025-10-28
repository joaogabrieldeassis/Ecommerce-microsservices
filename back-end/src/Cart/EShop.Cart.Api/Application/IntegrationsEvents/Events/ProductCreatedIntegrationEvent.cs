using EShop.Shared.EventBus.Events;

namespace EShop.Cart.Api.Application.IntegrationsEvents.Events;

public record ProductCreatedIntegrationEvent : IntegrationEvent
{
    public ProductCreatedIntegrationEvent(string name, int quantityInStock, decimal price)
    {
        Name = name;
        QuantityInStock = quantityInStock;
        Price = price;
    }

    public string Name { get; } = string.Empty;
    public int QuantityInStock { get; }
    public decimal Price { get; }
}