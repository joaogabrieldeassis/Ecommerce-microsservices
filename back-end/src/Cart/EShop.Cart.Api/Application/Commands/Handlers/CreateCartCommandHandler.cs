namespace EShop.Cart.Api.Application.Commands.Handlers;

public class CreateCartCommandHandler(INotifier notifier,
                                      CartContext context) : CommandHandlerBase(notifier), IRequestHandler<CreateCartCommand>
{
    private readonly CartContext _context = context;    

    public async Task Handle(CreateCartCommand request, CancellationToken cancellationToken)
    {
        var cart = new Models.Cart(request.UserId);
        var product = await _context.Products
                                    .AsNoTracking()
                                    .FirstOrDefaultAsync(p => p.Id == request.ProductId, cancellationToken);
        if(product == null)
        {
            Notify("Esse produto não está disponivel no momento");
            return;
        }

        var productCart = new ProductCart(product.Id, product.Name, product.QuantityInStock, product.Price);
        cart.AddProduct(productCart);   
        _context.Carts.Add(cart);

        await _context.CommitAsync();
    }
}