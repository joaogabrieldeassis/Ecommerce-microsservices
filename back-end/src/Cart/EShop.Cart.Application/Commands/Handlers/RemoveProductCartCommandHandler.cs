namespace EShop.Cart.Application.Commands.Handlers;

public class RemoveProductCartCommandHandler(INotifier notifier,
                                             IHttpContextAccessor httpContext,
                                             CartContext context) : CommandHandlerBase(notifier, httpContext), IRequestHandler<RemoveProductCartCommand>
{
    private readonly CartContext _context = context;

    public async Task Handle(RemoveProductCartCommand request, CancellationToken cancellationToken)
    {
        var cart = await _context.Carts
                                 .Include(c => c.Products)
                                 .FirstAsync(c => c.UserId == GetUserId() && !c.IsDeleted, cancellationToken);

        var productCart = cart.Products.FirstOrDefault(p => p.ProductId == request.ProductId)!;
        cart.RemoveProduct(productCart);
        _context.ProductsCarts.Remove(productCart);

        await _context.CommitAsync(cancellationToken);
    }
}