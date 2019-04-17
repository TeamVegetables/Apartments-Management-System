using System.Collections.Generic;

namespace AMS.Core.Models
{
    public class PaymentStatus
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public ICollection<Payment> Payments { get; set; }
    }
}
