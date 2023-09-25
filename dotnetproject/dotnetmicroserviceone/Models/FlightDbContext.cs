using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace dotnetmicroserviceone.Models;

public class FlightDbContext : DbContext
{
    public FlightDbContext()
    {
    }

    public FlightDbContext(DbContextOptions<FlightDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Flight> Flights { get; set; }

}
