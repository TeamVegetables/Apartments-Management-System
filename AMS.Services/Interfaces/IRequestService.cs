using System.Collections.Generic;
using System.Threading.Tasks;
using AMS.Core.Models;

namespace AMS.Services.Interfaces
{
    public interface IRequestService
    {
        Task<Request> GetRequestAsync(int id);

        Task<ICollection<Request>> GetAllRequestsAsync();

        Task<ICollection<Request>> GetRequestsByInhabitantId(string inhabitantId);

        Task<ICollection<Request>> GetRequestsByResolverId(string resolverId);

        Task AddRequestAsync(Request request);

        Task UpdateRequestAsync(Request request);

        Task RemoveRequestAsync(Request request);
    }
}