using EShop.Cart.Api.Application.IntegrationsEvents.Events;
using EShop.Shared.EventBus.Abstraction;

namespace EShop.Cart.Api.Application.IntegrationsEvents.Handlers;

public class ProductDeletedIntegrationEventHandler(CartContext context) : IIntegrationEventHandler<ProductDeletedIntegrationEvent>
{
    private readonly CartContext _context = context;

    public async Task Handle(ProductDeletedIntegrationEvent @event)
    {
        var product = await _context.Products.FindAsync(@event.ProductId);
        var carts = _context.Carts.Where(c => c.Products.Any(p => p.ProductId == @event.ProductId)).ToList();

        foreach (var cart in carts)
        {
            var productCart = cart.Products.FirstOrDefault(p => p.ProductId == @event.ProductId);
            if (productCart != null)
            {
                cart.RemoveProduct(productCart);
            }
        }
        _context.Products.Remove(product!);

        await _context.SaveChangesAsync();
    }
}
