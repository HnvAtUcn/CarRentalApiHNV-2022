using System;
using System.Collections.Generic;

#nullable disable

namespace CarRentalApiHNV.Models
{
    public partial class Car
    {
        public int CarId { get; set; }
        public string Brand { get; set; }
        public string Color { get; set; }
        public bool Active { get; set; }
    }
}
