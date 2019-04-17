using AMS.Core.Interfaces;
using AMS.Core.Models;

namespace AMS.Core.Repositories
{
    public class ApartmentRepository: BaseRepository<Apartment>, IApartmentRepository
    {
        public ApartmentRepository(ApplicationDbContext context): base(context)
        {
        }
    }
}