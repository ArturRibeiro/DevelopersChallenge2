using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Nibo.Prova.Asp.Web.Presentation.Application.Notifications
{
    public class DomainNotificationHandler : INotificationHandler<DomainNotification>
    {
        private List<DomainNotification> _notifications;

        public DomainNotificationHandler()
            => _notifications = new List<DomainNotification>();

        public Task Handle(DomainNotification message, CancellationToken cancellationToken)
        {
            _notifications.Add(message);

            return Task.CompletedTask;
        }

        public IList<DomainNotification> GetNotifications() => _notifications;
        public bool HasNotifications => GetNotifications().Any();
    }
}
