using System.Collections.Generic;
using System.Threading.Tasks;
using AMS.Core.Interfaces;
using AMS.Core.Models;
using AMS.Services.Interfaces;

namespace AMS.Services.Services
{
    public class ApartmentService : BaseService, IApartmentService
    {
        public ApartmentService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<Apartment> GetApartmentAsync(int id)
        {
            return await _uow.Apartments.GetAsync(id);
        }

        public async Task<ICollection<Apartment>> GetAllApartmentsAsync()
        {
            return await _uow.Apartments.GetAllAsync();
        }

        public async Task AddApartmentAsync(Apartment apartment)
        {
            await _uow.Apartments.AddAsync(apartment);
            await _uow.SaveAsync();
        }

        public async Task UpdateApartmentAsync(Apartment apartment)
        {
            await _uow.Apartments.UpdateAsync(apartment);
            await _uow.SaveAsync();
        }

        public async Task RemoveApartmentAsync(Apartment apartment)
        {
            await _uow.Apartments.RemoveAsync(apartment);
            await _uow.SaveAsync();
        }        
    }
}