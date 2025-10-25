namespace EShop.Cart.Api.Application.Queries.Commands;

public record GetCartUserCommand(Guid UserId) : IRequest<Models.Cart>;