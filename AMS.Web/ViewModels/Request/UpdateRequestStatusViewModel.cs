using AMS.Core.Models;

namespace AMS.Web.ViewModels.Request
{
    public class UpdateRequestStatusViewModel
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public RequestStatus Status { get; set; }
    }
}