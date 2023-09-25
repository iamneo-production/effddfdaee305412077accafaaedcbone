using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace dotnetmicroservicetwo.Models;

public class AirportDbContext : DbContext
{

    public AirportDbContext(DbContextOptions<AirportDbContext> options)
        : base(options)
    {
    }
    public virtual DbSet<Airport> Airports { get; set; }
}
