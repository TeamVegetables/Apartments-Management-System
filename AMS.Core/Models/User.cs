using System;
using Microsoft.AspNetCore.Identity;

namespace AMS.Core.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public int ApartmentId { get; set; }

        public int? ManagerId { get; set; }
    }
}
