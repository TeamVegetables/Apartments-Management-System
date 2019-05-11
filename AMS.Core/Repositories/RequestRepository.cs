using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AMS.Core.Interfaces;
using AMS.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace AMS.Core.Repositories
{
    public class RequestRepository: BaseRepository<Request>, IRequestRepository
    {
        public RequestRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<ICollection<Request>> GetRequestsByInhabitantId(string inhabitantId)
        {
            return await _context.Requests
                .Where(c=>c.InitiatorId==inhabitantId)
                .ToListAsync();
        }

        public async Task<ICollection<Request>> GetRequestsByResolverId(string resolverId)
        {
            return await _context.Requests
                .Where(r => r.ResolverId == resolverId)
                .ToListAsync();
        }
    }
}