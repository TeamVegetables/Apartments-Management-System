using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace AMS.DataAccess.Entities
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public Apartment Apartment { get; set; }

        public int ApartmentId { get; set; }

        public ICollection<Payment> Payments { get; set; }

        public ICollection<Request> Requests { get; set; }
    }
}
