using EShop.Cart.Api.Application.IntegrationsEvents.Events;
using EShop.Shared.EventBus.Abstraction;

namespace EShop.Cart.Api.Application.IntegrationsEvents.Handlers;

public class ProductCreatedIntegrationEventHandler : IIntegrationEventHandler<ProductCreatedIntegrationEvent>
{
    private readonly CartContext _context;

    public async Task Handle(ProductCreatedIntegrationEvent @event)
    {
        Console.WriteLine("T");
    }
}