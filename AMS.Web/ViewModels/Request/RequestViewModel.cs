using System;
using AMS.Core.Models;

namespace AMS.Web.ViewModels.Request
{
    public class RequestViewModel
    {
        public int Id { get; set; }

        public RequestStatus Status { get; set; }

        public string InitiatorId { get; set; }

        public string InitiatorName { get; set; }

        public string ResolverId { get; set; }

        public string ResolverName { get; set; }

        public string Message { get; set; }

        public DateTime Initiated { get; set; }

        public DateTime? Completed { get; set; }
    }
}