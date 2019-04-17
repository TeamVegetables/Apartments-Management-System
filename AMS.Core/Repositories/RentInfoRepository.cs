using AMS.Core.Interfaces;
using AMS.Core.Models;

namespace AMS.Core.Repositories
{
    public class RentInfoRepository : BaseRepository<RentInfo>, IRentInfoRepository
    {
        public RentInfoRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}