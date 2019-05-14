using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AMS.Core.Interfaces;
using AMS.Core.Models;
using AMS.Services.Interfaces;

namespace AMS.Services.Services
{
    public class NotificationService : BaseService, INotificationService
    {
        public NotificationService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task Create(string message, string userId)
        {
            var notification = new Notification
            {
                Date = DateTime.Now,
                Dismissed = false,
                Message = message,
                UserId = userId
            };
            await _uow.Notifications.Create(notification);
            await _uow.SaveAsync();
        }

        public IEnumerable<Notification> GetByUserId(string userId)
        {
            return _uow.Notifications.GetByUserId(userId);
        }

        public async void Dismiss(int notificationId)
        {
            var notification = _uow.Notifications.Get(notificationId);
            notification.Dismissed = true;
            await _uow.Notifications.UpdateAsync(notification);
            _uow.Save();
        }
    }
}
