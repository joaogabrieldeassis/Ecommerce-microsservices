namespace EShop.Cart.Application.Queries;

public class CartQuerieApplication(CartContext context, IHttpContextAccessor httpContext) : ICartQuerieApplication
{
    private readonly CartContext _context = context;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContext;

    public async Task<Domain.AggregatesModel.CartAggregate.Cart?> GetCartUserAsync(CancellationToken cancellationToken)
    {
        return await _context.Carts
                             .Include(x => x.Products)
                             .AsNoTracking()
                             .FirstOrDefaultAsync(c => c.UserId == GetUserId() && !c.IsDeleted, cancellationToken);
    }

    private Guid GetUserId()
    {
        return Guid.Parse(_httpContextAccessor.HttpContext!.User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
    }
}