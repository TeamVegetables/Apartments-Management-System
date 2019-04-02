using System.Collections.Generic;

namespace AMS.DataAccess.Entities
{
    public class RequestStatus
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int RequestId { get; set; }

        public ICollection<Payment> Requests { get; set; }

    }
}
