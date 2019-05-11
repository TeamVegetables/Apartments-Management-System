using System;
using AMS.Core.Models;

namespace AMS.Web.ViewModels.Payments
{
    public class ChangePaymentViewModel
    {
        public int PaymentId { get; set; }
        public int ApartmentId { get; set; }
        public PaymentStatus NewStatus { get; set; }
        public DateTime DeadLine { get; set; }
    }
}
