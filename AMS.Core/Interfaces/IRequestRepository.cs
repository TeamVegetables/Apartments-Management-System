using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using AMS.Core.Models;

namespace AMS.Core.Interfaces
{
    public interface IRequestRepository : IGenericRepository<Request>
    {
        Task<ICollection<Request>> GetRequestsByInhabitantId(string inhabitantId);
        Task<ICollection<Request>>  GetRequestsByResolverId(string resolverId);
    }
}
