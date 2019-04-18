using System.Collections.Generic;
using System.Threading.Tasks;
using AMS.Core.Models;

namespace AMS.Services.Interfaces
{
    public interface IRentInfoService
    {
        Task<RentInfo> GetRentInfoAsync(int id);
        Task<ICollection<RentInfo>> GetAllRentInfosAsync();

        Task AddRentInfoAsync(RentInfo rentInfo);
        Task UpdateRentInfoAsync(RentInfo rentInfo);
        Task RemoveRentInfoAsync(RentInfo rentInfo);
    }
}