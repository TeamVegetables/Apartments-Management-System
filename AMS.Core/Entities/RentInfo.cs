using System;

namespace AMS.DataAccess.Entities
{
    public class RentInfo
    {
        public int Id { get; set; }

        public User User { get; set; }

        public int UserId { get; set; }

        public Apartment Apartment { get; set; }

        public int ApartmentId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
