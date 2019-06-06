using System;

namespace AMS.Core.Models
{
   public class Request
    {
        public int Id { get; set; }

        public RequestStatus Status { get; set; }

        public string InitiatorId { get; set; }

        public string ResolverId { get; set; }

        public string Message { get; set; }

        public int? ApartmentId { get; set; }

        public DateTime Initiated { get; set; }

        public DateTime? Completed { get; set; } 
    }
}
