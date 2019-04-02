using System.Collections.Generic;

namespace AMS.DataAccess.Entities
{
    public class PaymentStatus
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int PaymentId { get; set; }

        public ICollection<Payment> Payments { get; set; }
    }
}
