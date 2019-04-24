using AMS.Core.Models;

namespace AMS.Web.ViewModels.Apartments
{
    public class CreateApartmentViewModel
    {
        public string Title { get; set; }

        public int Capacity { get; set; }

        public int Busy { get; set; }

        public ApartmentStatus Status { get; set; }
    }
}
