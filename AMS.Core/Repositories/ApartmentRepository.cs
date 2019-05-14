using System.Collections.Generic;
using System.Linq;
using AMS.Core.Interfaces;
using AMS.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace AMS.Core.Repositories
{
    public class ApartmentRepository : BaseRepository<Apartment>, IApartmentRepository
    {
        public ApartmentRepository(ApplicationDbContext context): base(context)
        {
        }

        public IEnumerable<Apartment> GetAllWithUsers()
        {
            return _context.Apartments.Include(a => a.Inhabitants);
        }

        public Apartment GetWithUsers(int id)
        {
            return _context.Apartments.Include(a => a.Inhabitants).FirstOrDefault(a => a.Id == id);
        }
    }
}