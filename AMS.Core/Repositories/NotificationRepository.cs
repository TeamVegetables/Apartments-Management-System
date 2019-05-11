using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AMS.Core.Interfaces;
using AMS.Core.Models;

namespace AMS.Core.Repositories
{
    public class NotificationRepository : BaseRepository<Notification>, INotificationRepository
    {
        public NotificationRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Notification> Create(Notification notification)
        {
            var result = await _context.Notifications.AddAsync(notification);
            return result.Entity;
        }

        public Notification Get(int id)
        {
            return _context.Notifications.Find(id);
        }

        public IEnumerable<Notification> GetAll()
        {
            return _context.Notifications.ToList();
        }

        public IEnumerable<Notification> GetByUserId(string userId)
        {
            var notifications = _context.Notifications.ToList().FindAll(n => n.UserId == userId && !n.Dismissed);
            return notifications;
        }
    }
}
