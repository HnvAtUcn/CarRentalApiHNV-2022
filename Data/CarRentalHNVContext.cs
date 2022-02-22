using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CarRentalApiHNV.Models;

namespace CarRentalApiHNV.Data
{
    public class CarRentalApiHNVContext : DbContext
    {
        public CarRentalApiHNVContext (DbContextOptions<CarRentalApiHNVContext> options)
            : base(options)
        {
        }

        public DbSet<CarRentalApiHNV.Models.Car> Car { get; set; }
    }
}
