namespace EShop.Cart.Api.Application.Commands.Handlers;

public class CommandHandlerBase(INotifier notificador)
{
    private readonly INotifier _notifier = notificador;

    protected bool TheEntityIsValid<TV, TE>(TV validacao, TE entidade) where TV : AbstractValidator<TE> where TE : Entity
    {
        ValidationResult _validacao = validacao.Validate(entidade);

        if (_validacao.IsValid)
            return true;

        Notify(_validacao);

        return false;
    }

    protected void Notify(ValidationResult validationResult)
    {
        foreach (var error in validationResult.Errors)
        {
            Notify(error.ErrorMessage);
        }
    }

    protected void Notify(string menssagem)
    {
        _notifier.Handle(new Notification(menssagem));
    }
}