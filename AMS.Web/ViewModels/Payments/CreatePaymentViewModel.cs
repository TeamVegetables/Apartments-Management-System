using System;
using System.ComponentModel.DataAnnotations;

namespace AMS.Web.ViewModels.Payments
{
    public class CreatePaymentViewModel
    {
        [Required]
        public decimal Sum { get; set; }

        [Required]
        public DateTime DeadLine { get; set; }

        [Required]
        public int ApartmentId { get; set; }
    }
}
