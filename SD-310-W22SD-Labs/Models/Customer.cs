﻿using System;
using System.Collections.Generic;

namespace SD_310_W22SD_Labs.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Rentals = new HashSet<Rental>();
        }

        public int Id { get; set; }
        public string UserName { get; set; } = null!;
        public int RentalHrs { get; set; }

        public virtual ICollection<Rental> Rentals { get; set; }
    }
}
