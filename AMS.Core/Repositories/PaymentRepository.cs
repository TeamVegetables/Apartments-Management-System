using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AMS.Core.Interfaces;
using AMS.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace AMS.Core.Repositories
{
    public class PaymentRepository : BaseRepository<Payment>, IPaymentRepository
    {
        public PaymentRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<ICollection<Payment>> GetPaymentsByApartment(int apartmentId)
        {
            return await _context.Payments
                .Include(p => p.Apartment)
                .Where(r => r.ApartmentId == apartmentId)
                .ToListAsync();
        }
    }
}