using EShop.Shared.Interfaces;
using EShop.Shared.Notifications;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace EShop.Shared.Api.Controllers;

[ApiController]
public abstract class MainController(INotifier notifier) : ControllerBase
{
    private readonly INotifier _notifier = notifier;

    public Guid UserId { get; set; }
    public bool IsUserAuthenticated { get; set; }

    protected bool IsOperationValid()
    {
        return !_notifier.HasNotification();
    }

    protected ActionResult CustomResponse(object? result = null)
    {
        if (IsOperationValid())
        {
            return Ok(new
            {
                success = true,
                data = result
            });
        }

        return BadRequest(new
        {
            success = false,
            errors = _notifier.GetNotifications().Select(n => n.Message)
        });
    }

    protected ActionResult CustomResponse(ModelStateDictionary modelState)
    {
        if (!modelState.IsValid) NotifyInvalidModelErrors(modelState);
        return CustomResponse();
    }

    protected void NotifyInvalidModelErrors(ModelStateDictionary modelState)
    {
        var errors = modelState.Values.SelectMany(v => v.Errors);

        foreach (var error in errors)
        {
            var errorMessage = error.Exception == null
                ? error.ErrorMessage
                : error.Exception.Message;

            NotifyError(errorMessage);
        }
    }

    protected void NotifyError(string message)
    {
        _notifier.Handle(new Notification(message));
    }
}