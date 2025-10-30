using EShop.Cart.Api.Application.IntegrationsEvents.Events;
using EShop.Shared.EventBus.Abstraction;

namespace EShop.Cart.Api.Application.IntegrationsEvents.Handlers;

public class ProductUpdatedIntegrationEventHandler(CartContext context) : IIntegrationEventHandler<ProductUpdatedIntegrationEvent>
{
    private readonly CartContext _context = context;

    public async Task Handle(ProductUpdatedIntegrationEvent @event)
    {
        var product = await _context.Products.FindAsync(@event.ProductId);        
        var carts = _context.Carts.Where(c => c.Products.Any(p => p.ProductId == @event.ProductId)).ToList();

        foreach (var cart in carts)
        {
            var productCart = cart.Products.FirstOrDefault(p => p.ProductId == @event.ProductId);
            productCart?.UpdateDetails(@event.Name, @event.QuantityInStock, @event.Price);
        }

        product?.UpdateDetails(@event.Name, @event.QuantityInStock, @event.Price);

        await _context.SaveChangesAsync();
    }
}