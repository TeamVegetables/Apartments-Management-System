﻿using System.Collections.Generic;

namespace AMS.Core.Models
{
    public class ApartmentStatus
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public ICollection<Apartment> Apartments { get; set; }
    }
}