using System.Collections.Generic;

namespace AMS.Core.Models
{
    public class Apartment
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int Capacity { get; set; }

        public ApartmentStatus ApartmentStatus { get; set; }

        public int ApartmentStatusId { get; set; }
        
        public ICollection<Payment> Payments { get; set; }

        public ICollection<User> Users { get; set; }

        public ICollection<RentInfo> RentInfos { get; set; }
    }
}
