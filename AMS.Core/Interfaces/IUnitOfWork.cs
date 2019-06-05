using System;
using System.Threading.Tasks;

namespace AMS.Core.Interfaces
{
    public interface IUnitOfWork /*IDisposable*/
    {
        IApartmentRepository Apartments { get; }
        IPaymentRepository Payments { get; }
        IRentInfoRepository RentInfos { get; }
        IRequestRepository Requests { get; }
        INotificationRepository Notifications { get; }
        int Save();
        Task<int> SaveAsync();
    }
}