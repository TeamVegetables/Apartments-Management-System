using AMS.Core.Interfaces;
using AMS.Core.Models;

namespace AMS.Core.Repositories
{
    public class PaymentRepository : BaseRepository<Payment>, IPaymentRepository
    {
        public PaymentRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}