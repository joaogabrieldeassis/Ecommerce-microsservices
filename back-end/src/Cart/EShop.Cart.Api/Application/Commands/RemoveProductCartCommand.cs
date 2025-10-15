namespace EShop.Cart.Api.Application.Commands;

public record RemoveProductCartCommand(Guid ProductId) : IRequest;