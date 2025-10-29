using EShop.Cart.Api.Application.IntegrationsEvents.Events;
using EShop.Shared.EventBus.Abstraction;

namespace EShop.Cart.Api.Application.IntegrationsEvents.Handlers;

public class ProductCreatedIntegrationEventHandler(CartContext context) : IIntegrationEventHandler<ProductCreatedIntegrationEvent>
{
    private readonly CartContext _context = context;

    public async Task Handle(ProductCreatedIntegrationEvent @event)
    {
        var product = new Product(@event.ProductId,
                                  @event.Name,
                                  @event.QuantityInStock,
                                  @event.Price);
        _context.Products.Add(product);

        await _context.SaveChangesAsync();
    }
}