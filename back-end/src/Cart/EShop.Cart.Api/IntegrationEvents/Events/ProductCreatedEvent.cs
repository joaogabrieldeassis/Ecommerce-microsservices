namespace EShop.Cart.Api.IntegrationEvents.Events;

public class ProductCreatedEvent
{
    public string Name { get; private set; } = string.Empty;
    public int QuantityInStock { get; private set; }
    public decimal Price { get; private set; }
}