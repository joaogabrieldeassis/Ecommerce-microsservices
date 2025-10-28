namespace EShop.Cart.Api.Application.Commands.Handlers;

public class RemoveProductCartCommandHandler(INotifier notifier,
                                             IHttpContextAccessor httpContext) : CommandHandlerBase(notifier, httpContext), IRequestHandler<RemoveProductCartCommand>
{
    public Task Handle(RemoveProductCartCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}