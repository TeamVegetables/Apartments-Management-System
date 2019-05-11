using System.Collections.Generic;
using System.Threading.Tasks;
using AMS.Core.Models;

namespace AMS.Services.Interfaces
{
    public interface IPaymentService
    {
        Task<Payment> GetPaymentAsync(int id);
        Task<ICollection<Payment>> GetPaymentsByApartment(int apartmentId);
        Task<ICollection<Payment>> GetAllPaymentsAsync();

        Task AddPaymentAsync(Payment payment);
        Task UpdatePaymentAsync(Payment payment);
        Task RemovePaymentAsync(Payment payment);
        Task ChangeStatus(Payment payment);

    }
}