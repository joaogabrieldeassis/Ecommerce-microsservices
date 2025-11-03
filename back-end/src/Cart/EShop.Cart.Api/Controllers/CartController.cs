using EShop.Cart.Application.Commands;
namespace EShop.Cart.Api.Controllers;

[Route("api/[controller]")]
[Authorize]
public class CartController(INotifier notifier,
                            IMediator mediator,
                            ICartQuerieApplication cartQuerie) : MainController(notifier)
{
    private readonly IMediator _mediator = mediator;
    private readonly ICartQuerieApplication _cartQuerie = cartQuerie;

    [HttpGet("get-cart")]
    public async Task<ActionResult> GetCartAsync(CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);
        var cart = await _cartQuerie.GetCartUserAsync(cancellationToken);

        return Ok(cart);
    }

    [HttpPost("add-item-cart")]
    public async Task<ActionResult> AddItemCartAsync(AddProductInCartCommand command, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);
        var cart = await _cartQuerie.GetCartUserAsync(cancellationToken);

        if (cart == null)
        {
            await _mediator.Send(new CreateCartCommand(command.ProductId));
            return CustomResponse();
        }

        await _mediator.Send(command, cancellationToken);
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