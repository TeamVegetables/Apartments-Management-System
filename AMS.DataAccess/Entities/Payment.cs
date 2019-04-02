using System;

namespace AMS.DataAccess.Entities
{
    public class Payment
    {
        public int Id { get; set; }

        public int ApartmentId { get; set; }

        public int UserId { get; set; }

        public int StatusId { get; set; }

        public decimal Sum { get; set; }

        public DateTime Initiated { get; set; }

        public DateTime Completed { get; set; }
    }
}
