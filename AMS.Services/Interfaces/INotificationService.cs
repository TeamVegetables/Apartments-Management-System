using System.Collections.Generic;
using System.Threading.Tasks;
using AMS.Core.Models;

namespace AMS.Services.Interfaces
{
    public interface INotificationService
    {
        Task<Notification> Create(string message, string userId);

        IEnumerable<Notification> GetByUserId(string userId);

        void Dismiss(int notificationId);
    }
}