using EShop.Shared.Notifications;

namespace EShop.Shared.Interfaces;

public interface INotifier
{
    bool HasNotification();
    List<Notification> GetNotifications();
    void Handle(Notification notification);
}