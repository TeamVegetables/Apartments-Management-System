using System.Threading.Tasks;
using AMS.Core.Interfaces;
using AMS.Core.Models;

namespace AMS.Core.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context,
            IPaymentRepository paymentRepository,
            IRentInfoRepository rentInfoRepository,
            IRequestRepository requestRepository,
            IApartmentRepository apartmentRepository,
            INotificationRepository notificationRepository)
        {
            Payments = paymentRepository;
            RentInfos = rentInfoRepository;
            Requests = requestRepository;
            Apartments = apartmentRepository;
            Notifications = notificationRepository;
            _context = context;
        }


        public virtual IApartmentRepository Apartments { get; }

        public virtual IPaymentRepository Payments { get; }

        public virtual IRentInfoRepository RentInfos { get; }

        public virtual IRequestRepository Requests { get; }

        public virtual INotificationRepository Notifications { get; }

        //public void Dispose()
        //{
        //    _context.Dispose();
        //}

        public int Save()
        {
            return _context.SaveChanges();
        }

        public Task<int> SaveAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}