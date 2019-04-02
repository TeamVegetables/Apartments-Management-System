using System;

namespace AMS.DataAccess.Entities
{
    public class Payment
    {
        public int Id { get; set; }

        public Apartment Apartment { get; set; }

        public int ApartmentId { get; set; }

        public User User { get; set; }

        public int UserId { get; set; }

        public PaymentStatus PaymentStatus { get; set; }

        public int PaymentStatusId { get; set; }

        public decimal Sum { get; set; }

        public DateTime Initiated { get; set; }

        public DateTime Completed { get; set; }
    }
}
