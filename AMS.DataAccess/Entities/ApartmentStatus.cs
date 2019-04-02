using System.Collections.Generic;

namespace AMS.DataAccess.Entities
{
    public class ApartmentStatus
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public ICollection<Apartment> Apartments { get; set; }
    }
}
