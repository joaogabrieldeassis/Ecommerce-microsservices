namespace EShop.Cart.Api.Application.Commands;

public record AddProductInCartCommand(Guid ProductId) : IRequest;