using System;
using AMS.Core.Models;

namespace AMS.Web.ViewModels.Request
{
    public class CreateRequestViewModel
    {
        public int Id { get; set; }

        public string Message { get; set; }

        public string InitiatorId { get; set; }

        public string ResolverId { get; set; }

        public RequestStatus Status { get; set; }
        
        public DateTime Initiated { get; set; }
    }
}
