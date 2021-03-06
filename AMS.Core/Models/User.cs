﻿using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace AMS.Core.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Apartment Apartment { get; set; }

        public int? ApartmentId { get; set; }

        public string ManagerId { get; set; }

        public bool IsLocked { get; set; }

        public DateTime? RentEndDate { get; set; }

        public List<Notification> Notifications { get; set; }
    }
}
