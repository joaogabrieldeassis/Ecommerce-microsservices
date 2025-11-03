namespace EShop.Cart.Application.Commands;

public record DecreaseQuantityProductCartCommand(Guid ProductId) : IRequest;