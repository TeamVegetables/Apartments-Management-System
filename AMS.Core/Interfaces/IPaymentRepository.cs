using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using AMS.Core.Models;

namespace AMS.Core.Interfaces
{
    public interface IPaymentRepository: IGenericRepository<Payment>
    {
        Task<ICollection<Payment>> GetPaymentsByApartment(int apartmentId);
    }
}
