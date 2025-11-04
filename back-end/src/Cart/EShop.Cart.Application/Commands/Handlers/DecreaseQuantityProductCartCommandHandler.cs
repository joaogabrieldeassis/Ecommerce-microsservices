namespace EShop.Cart.Application.Commands.Handlers;

public class DecreaseQuantityProductCartCommandHandler(INotifier notifier,
                                                       CartContext context,
                                                       IHttpContextAccessor httpContext)
    : CommandHandlerBase(notifier, httpContext), IRequestHandler<DecreaseQuantityProductCartCommand, Domain.AggregatesModel.CartAggregate.Cart>
{
    private readonly CartContext _context = context;

    public async Task<Domain.AggregatesModel.CartAggregate.Cart> Handle(DecreaseQuantityProductCartCommand request, CancellationToken cancellationToken)
    {
        var cart = await _context.Carts
                                 .Include(c => c.Products)
                                 .FirstAsync(c => c.UserId == GetUserId() && !c.IsDeleted, cancellationToken);

        cart.DecreaseQuantityProduct(request.ProductId);
        _context.Carts.Update(cart);

        await _context.CommitAsync(cancellationToken);

        return cart;
    }
}