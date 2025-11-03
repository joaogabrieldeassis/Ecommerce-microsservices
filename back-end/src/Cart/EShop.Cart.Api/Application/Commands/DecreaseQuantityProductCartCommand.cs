namespace EShop.Cart.Api.Application.Commands;

public record DecreaseQuantityProductCartCommand(Guid ProductId) : IRequest;