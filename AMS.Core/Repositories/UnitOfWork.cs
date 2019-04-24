using System.Threading.Tasks;
using AMS.Core.Interfaces;
using AMS.Core.Models;

namespace AMS.Core.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private  IPaymentRepository _paymentRepository;
        private  IRentInfoRepository _rentInfoRepository;
        private  IRequestRepository _requestRepository;
        private  IApartmentRepository _apartmentRepository;

        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }
        

        public IApartmentRepository Apartments => _apartmentRepository ?? (_apartmentRepository = new ApartmentRepository(_context));

        public IPaymentRepository Payments => _paymentRepository ?? (_paymentRepository = new PaymentRepository(_context));

        public IRentInfoRepository RentInfos => _rentInfoRepository ?? (_rentInfoRepository = new RentInfoRepository(_context));

        public IRequestRepository Requests => _requestRepository ?? (_requestRepository = new RequestRepository(_context));

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