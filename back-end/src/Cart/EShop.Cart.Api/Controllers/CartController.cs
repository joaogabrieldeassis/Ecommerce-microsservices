using EShop.Cart.Api.Application.Queries.Commands;
using EShop.Shared.Api.Controllers;
using Microsoft.AspNetCore.Authorization;

namespace EShop.Cart.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class CartController(INotifier notifier,
                            IMediator mediator, 
                            IHttpContextAccessor httpContext ) : MainController(notifier)
{
    private readonly IMediator _mediator = mediator;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContext;

    [HttpPost("add-item-cart")]
    public async Task<ActionResult> AddItemCart(AddProductInCartCommand command)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);
        var cart = await _mediator.Send(new GetCartUserCommand());

        if(cart == null)
        {
            await _mediator.Send(new CreateCartCommand(command.ProductId));
            return CustomResponse();
        }

        await _mediator.Send(command);
        return CustomResponse();
    }
}