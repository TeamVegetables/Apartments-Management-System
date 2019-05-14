using System.Threading.Tasks;
using AMS.Core.Interfaces;
using AMS.Core.Models;

namespace AMS.Core.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IRentInfoRepository _rentInfoRepository;
        private readonly IRequestRepository _requestRepository;
        private readonly IApartmentRepository _apartmentRepository;
        private readonly INotificationRepository _notificationRepository;

        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context,
            IPaymentRepository paymentRepository,
            IRentInfoRepository rentInfoRepository,
            IRequestRepository requestRepository,
            IApartmentRepository apartmentRepository,
            INotificationRepository notificationRepository)
        {
            _paymentRepository = paymentRepository;
            _rentInfoRepository = rentInfoRepository;
            _requestRepository = requestRepository;
            _apartmentRepository = apartmentRepository;
            _notificationRepository = notificationRepository;
            _context = context;
        }


        public IApartmentRepository Apartments => _apartmentRepository;

        public IPaymentRepository Payments => _paymentRepository;

        public IRentInfoRepository RentInfos => _rentInfoRepository;

        public IRequestRepository Requests => _requestRepository;

        public INotificationRepository Notifications => _notificationRepository;

        public void Dispose()
        {
            _context.Dispose();
        }

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