namespace EShop.Cart.Application.Commands;

public record AddProductInCartCommand(Guid ProductId) : IRequest;