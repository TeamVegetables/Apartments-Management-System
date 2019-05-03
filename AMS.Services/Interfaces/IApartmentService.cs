using System.Collections.Generic;
using System.Threading.Tasks;
using AMS.Core.Models;

namespace AMS.Services.Interfaces
{
    public interface IApartmentService
    {
        Task<Apartment> GetApartmentAsync(int id);
        Task<ICollection<Apartment>> GetAllApartmentsAsync();

        Task AddApartmentAsync(Apartment apartment);
        Task UpdateApartmentAsync(Apartment apartment);
        Task RemoveApartmentAsync(Apartment apartment);
        Apartment GetApartmentWithUsers(int id);
        IEnumerable<Apartment> GetAllWithUsers();
    }
}