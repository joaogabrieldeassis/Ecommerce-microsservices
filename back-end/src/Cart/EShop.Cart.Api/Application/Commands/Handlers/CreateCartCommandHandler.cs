namespace EShop.Cart.Api.Application.Commands.Handlers;

public class CreateCartCommandHandler(INotifier notifier) : CommandHandlerBase(notifier), IRequestHandler<CreateCartCommand>
{
    public Task Handle(CreateCartCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}