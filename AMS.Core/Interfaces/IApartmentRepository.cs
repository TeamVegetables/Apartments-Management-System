using System.Collections.Generic;
using AMS.Core.Models;

namespace AMS.Core.Interfaces
{
    public interface IApartmentRepository: IGenericRepository<Apartment>
    {
        IEnumerable<Apartment> GetAllWithUsers();
        Apartment GetWithUsers(int id);
    }
}
