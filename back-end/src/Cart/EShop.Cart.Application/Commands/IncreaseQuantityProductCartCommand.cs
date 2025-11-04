namespace EShop.Cart.Application.Commands;

public record IncreaseQuantityProductCartCommand(Guid ProductId) : IRequest<Domain.AggregatesModel.CartAggregate.Cart>;