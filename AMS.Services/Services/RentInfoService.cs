using AMS.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using AMS.Core.Interfaces;
using AMS.Core.Models;

namespace AMS.Services.Services
{
    public class RentInfoService: BaseService, IRentInfoService
    {
        public RentInfoService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<RentInfo> GetRentInfoAsync(int id)
        {
            return await _uow.RentInfos.GetAsync(id);
        }

        public async Task<ICollection<RentInfo>> GetAllRentInfosAsync()
        {
            return await _uow.RentInfos.GetAllAsync();
        }

        public async Task AddRentInfoAsync(RentInfo rentInfo)
        {
            await _uow.RentInfos.AddAsync(rentInfo);
            await _uow.SaveAsync();
        }

        public async Task UpdateRentInfoAsync(RentInfo rentInfo)
        {
            await _uow.RentInfos.UpdateAsync(rentInfo);
            await _uow.SaveAsync();
        }

        public async Task RemoveRentInfoAsync(RentInfo rentInfo)
        {
            await _uow.RentInfos.RemoveAsync(rentInfo);
            await _uow.SaveAsync();
        }
    }
}
