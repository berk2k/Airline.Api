using Airline.Api.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Airline.Api.Context
{
    public class AirlineDbContext : DbContext
    {
        public AirlineDbContext(DbContextOptions<AirlineDbContext> options)
            : base(options)
        {
        }

        public DbSet<Flight> Flights { get; set; }
        public DbSet<User> Users { get; set; }

        public DbSet<Ticket> Tickets { get; set; }

    }
}
