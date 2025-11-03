using EShop.Cart.Api.Application.Queries.Commands;
using EShop.Shared.Api.Controllers;
using Microsoft.AspNetCore.Authorization;

namespace EShop.Cart.Api.Controllers;

[Route("api/[controller]")]
[Authorize]
public class CartController(INotifier notifier,
                            IMediator mediator) : MainController(notifier)
{
    private readonly IMediator _mediator = mediator;

    [HttpGet("get-cart")]
    public async Task<ActionResult> GetCartAsync()
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);
        var cart = await _mediator.Send(new GetCartUserCommand());

        return CustomResponse(cart);
    }

    [HttpPost("add-item-cart")]
    public async Task<ActionResult> AddItemCartAsync(AddProductInCartCommand command)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);
        var cart = await _mediator.Send(new GetCartUserCommand());

        if (cart == null)
        {
            await _mediator.Send(new CreateCartCommand(command.ProductId));
            return CustomResponse();
        }

        await _mediator.Send(command);
        return CustomResponse();
    }

    [HttpPut("remove-item-cart")]
    public async Task<ActionResult> RemoveItemCartAsync(RemoveProductCartCommand command)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        await _mediator.Send(command);

        return CustomResponse();
    }
}