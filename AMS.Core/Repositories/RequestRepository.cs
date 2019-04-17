using AMS.Core.Interfaces;
using AMS.Core.Models;

namespace AMS.Core.Repositories
{
    public class RequestRepository: BaseRepository<Request>, IRequestRepository
    {
        public RequestRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}