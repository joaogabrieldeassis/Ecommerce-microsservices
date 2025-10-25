namespace EShop.Cart.Api.Application.Commands;

public record CreateCartCommand(Guid UserId, Guid ProductId) : IRequest;