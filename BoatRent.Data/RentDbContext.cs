using BoatRent.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoatRent.Data
{
    public class RentDbContext : DbContext
    {
        public RentDbContext(DbContextOptions option): base(option)
        {

        }
        public DbSet<Boat> Boats { get; set; }
        public DbSet<RentBoat> RentBoat {  get; set;}
    }
}
