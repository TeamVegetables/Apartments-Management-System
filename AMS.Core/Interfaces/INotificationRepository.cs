using System.Collections.Generic;
using System.Threading.Tasks;
using AMS.Core.Models;

namespace AMS.Core.Interfaces
{
    public interface INotificationRepository : IGenericRepository<Notification>
    {
        Task<Notification> Create(Notification notification);
        Notification Get(int id);
        IEnumerable<Notification> GetAll();
        IEnumerable<Notification> GetByUserId(string userId);
    }
}