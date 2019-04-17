using System.Collections.Generic;

namespace AMS.Core.Models
{
    public class RequestStatus
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public ICollection<Request> Requests { get; set; }
    }
}
