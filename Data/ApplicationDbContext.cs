using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using HOTEL1.Models;

namespace HOTEL1.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<HOTEL1.Models.Hotel> Hotel { get; set; }
        public DbSet<HOTEL1.Models.Customer> Customer { get; set; }
        public DbSet<HOTEL1.Models.CustomerofHotel> CustomerofHotel { get; set; }
        public DbSet<HOTEL1.Models.Room> Room { get; set; }
        public DbSet<HOTEL1.Models.Booked> Booked { get; set; }
    }
}
