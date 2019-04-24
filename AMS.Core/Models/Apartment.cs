using System.Collections.Generic;

namespace AMS.Core.Models
{
    public class Apartment
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int Capacity { get; set; }

        public int Busy { get; set; }

        public ApartmentStatus Status { get; set; }

        public List<User> Inhabitants { get; set; }
    }
}
