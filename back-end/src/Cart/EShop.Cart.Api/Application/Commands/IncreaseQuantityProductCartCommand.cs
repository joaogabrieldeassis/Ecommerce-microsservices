namespace EShop.Cart.Api.Application.Commands;

public record IncreaseQuantityProductCartCommand(Guid ProductId) : IRequest;
