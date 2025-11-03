namespace EShop.Cart.Application.Queries;

public interface ICartQuerieApplication
{
    Task<Domain.AggregatesModel.CartAggregate.Cart?> GetCartUserAsync(CancellationToken cancellationToken);
}