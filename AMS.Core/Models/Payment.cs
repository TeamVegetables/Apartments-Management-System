using System;

namespace AMS.Core.Models
{
    public class Payment
    {
        public int Id { get; set; }

        public int ApartmentId { get; set; }

        public PaymentStatus Status { get; set; }

        public decimal Sum { get; set; }

        public DateTime Initiated { get; set; }

        public DateTime DeadLine { get; set; }

        public DateTime Completed { get; set; }
    }
}
