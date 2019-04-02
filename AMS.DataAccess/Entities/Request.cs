using System;

namespace AMS.DataAccess.Entities
{
   public class Request
    {
        public int Id { get; set; }

        public int RequestStatusId { get; set; }

        public RequestStatus RequestStatus { get; set; }

        public User Initiator { get; set; }

        public int InitiatorId { get; set; }

        public User Resolver { get; set; }

        public int ResolverId { get; set; }

        public string Message { get; set; }

        public DateTime Initiated { get; set; }

        public DateTime Completed { get; set; }

    }
}
