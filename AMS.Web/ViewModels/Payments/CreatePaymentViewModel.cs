using System;

namespace AMS.Web.ViewModels.Payments
{
    public class CreatePaymentViewModel
    {
        public decimal Sum { get; set; }

        public DateTime DeadLine { get; set; }
    }
}
