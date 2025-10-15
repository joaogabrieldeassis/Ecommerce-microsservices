namespace EShop.Cart.Api.Application.Commands.Handlers;

public class AddProductInCartCommandHandler(INotifier notifier) : CommandHandlerBase(notifier), IRequestHandler<AddProductInCartCommand>
{
    public Task Handle(AddProductInCartCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}