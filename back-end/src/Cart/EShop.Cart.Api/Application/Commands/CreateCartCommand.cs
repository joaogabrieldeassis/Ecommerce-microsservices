namespace EShop.Cart.Api.Application.Commands;

public record CreateCartCommand(Guid ProductId) : IRequest;