namespace EShop.Cart.Application.Commands;

public record CreateCartCommand(Guid ProductId) : IRequest;