using EShop.Cart.Api.Application.Queries.Commands;

namespace EShop.Cart.Api.Application.Queries;

public class GetCartUserQuerie(CartContext context) : IRequestHandler<GetCartUserCommand, Models.Cart?>
{
    private readonly CartContext _context = context;

    public async Task<Models.Cart?> Handle(GetCartUserCommand request, CancellationToken cancellationToken)
    {
        return await _context.Carts
                             .Include(x => x.Products)
                             .AsNoTracking()
                             .FirstOrDefaultAsync(c => c.UserId == request.UserId && !c.IsDeleted, cancellationToken);
    }
}