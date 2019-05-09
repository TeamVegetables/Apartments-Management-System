using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AMS.Core.Models;

namespace AMS.Web.ViewModels.Payments
{
    public class ShowPaymentsViewModel
    {
        public ICollection<Payment> Payments { get; set; }

        public Apartment Apartment { get; set; }
    }
}
