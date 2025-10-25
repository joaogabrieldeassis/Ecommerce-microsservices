using System.Security.Claims;

namespace EShop.Cart.Api.Application.Queries;

public class CommandQuerieBase(IHttpContextAccessor httpContext)
{
    private readonly IHttpContextAccessor _httpContextAccessor = httpContext;

    protected Guid GetUserId()
    {
        return Guid.Parse(_httpContextAccessor.HttpContext!.User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
    }
}