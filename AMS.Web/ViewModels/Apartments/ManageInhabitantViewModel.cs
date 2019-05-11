using System;

namespace AMS.Web.ViewModels.Apartments
{
    public class ManageInhabitantViewModel
    {
        public string InhabitantId { get; set; }
        public int ApartmentId { get; set; }
        public DateTime RentEndDate { get; set; }
    }
}
