namespace EShop.Cart.Api.Application.Commands.Handlers;

public class AddProductInCartCommandHandler(INotifier notifier,
                                            CartContext context,
                                            IHttpContextAccessor httpContext) : CommandHandlerBase(notifier, httpContext), IRequestHandler<AddProductInCartCommand>
{
    private readonly CartContext _context = context;

    public async Task Handle(AddProductInCartCommand request, CancellationToken cancellationToken)
    {
        var cart = await _context.Carts
                                 .Include(c => c.Products)
                                 .FirstAsync(c => c.UserId == GetUserId() && !c.IsDeleted, cancellationToken);

        var product = await _context.Products
                                    .AsNoTracking()
                                    .FirstOrDefaultAsync(p => p.Id == request.ProductId, cancellationToken);
        if (product == null)
        {
            Notify("Esse produto não está disponivel no momento.");
            return;
        }

        var productCart = new ProductCart(product.Id, product.Name, product.QuantityInStock, product.Price);
        cart.AddProduct(productCart);

        await _context.CommitAsync(cancellationToken);
    }
}