using System;

namespace AMS.Core.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public DateTime Date { get; set; }
        public string Message { get; set; }
        public bool Dismissed { get; set; }
    }
}
