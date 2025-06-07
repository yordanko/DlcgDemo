using System;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    { 
    }

    public required DbSet<VideoGame> VideoGames { get; set; }

    // Define DbSet properties for your entities here
    // Example: public DbSet<VideoGame> VideoGames { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        // Configure entity properties and relationships here
    }
}
