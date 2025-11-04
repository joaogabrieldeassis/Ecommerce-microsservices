namespace EShop.Cart.Application.Commands.Handlers;

public class IncreaseQuantityProductCartCommandHandler(INotifier notifier,
                                                       CartContext context,
                                                       IHttpContextAccessor httpContext)
    : CommandHandlerBase(notifier, httpContext), IRequestHandler<IncreaseQuantityProductCartCommand, Domain.AggregatesModel.CartAggregate.Cart>
{
    private readonly CartContext _context = context;

    public async Task<Domain.AggregatesModel.CartAggregate.Cart> Handle(IncreaseQuantityProductCartCommand request, CancellationToken cancellationToken)
    {
        var cart = await _context.Carts
                                 .Include(c => c.Products)
                                 .FirstAsync(c => c.UserId == GetUserId() && !c.IsDeleted, cancellationToken);

        cart.IncreaseQuantityProduct(request.ProductId);
        _context.Carts.Update(cart);

        await _context.CommitAsync(cancellationToken);

        return cart;
    }
}
