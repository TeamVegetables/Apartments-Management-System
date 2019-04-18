using System.Collections.Generic;
using System.Threading.Tasks;
using AMS.Core.Interfaces;
using AMS.Core.Models;
using AMS.Services.Interfaces;

namespace AMS.Services.Services
{
    public class RequestService: BaseService, IRequestService
    {

        public RequestService(IUnitOfWork unitOfWork): base(unitOfWork)
        {
        }

        public async Task<Request> GetRequestAsync(int id)
        {
            return await _uow.Requests.GetAsync(id);
        }

        public async Task<ICollection<Request>> GetAllRequestsAsync()
        {
            return await _uow.Requests.GetAllAsync();
        }

        public async Task AddRequestAsync(Request request)
        {
            await _uow.Requests.AddAsync(request);
        }

        public async Task UpdateRequestAsync(Request request)
        {
            await _uow.Requests.UpdateAsync(request);
        }

        public async Task RemoveRequestAsync(Request request)
        {
            await _uow.Requests.RemoveAsync(request);
        }
    }
}