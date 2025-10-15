using EShop.Shared.Interfaces;

namespace EShop.Shared.Notifications;

public class Notifier : INotifier
{
    private readonly List<Notification> _notifications;

    public Notifier()
    {
        _notifications = [];
    }

    public void Handle(Notification notification)
    {
        _notifications.Add(notification);
    }

    public List<Notification> GetNotifications()
    {
        return _notifications;
    }

    public bool HasNotification()
    {
        return _notifications.Count != 0;
    }
}