using EShop.Cart.Api.Application.Queries.Commands;
using EShop.Shared.Api.Controllers;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

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
        var cart = await _mediator.Send(new GetCartUserCommand(GetUserId()));

        if(cart == null || !cart.HasProduct())
        {
            await _mediator.Send(new CreateCartCommand(GetUserId()));
            return CustomResponse();
        }

        await _mediator.Send(command);

        return CustomResponse();
    }
    
    private Guid GetUserId()
    {
        return Guid.Parse(_httpContextAccessor.HttpContext!.User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
    }
}