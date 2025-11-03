namespace EShop.Cart.Application.Commands;

public record RemoveProductCartCommand(Guid ProductId) : IRequest;