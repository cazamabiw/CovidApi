using Microsoft.EntityFrameworkCore;
using CovidApi.Data.Models;

namespace CovidApi.Data;

public class CovidDbContext : DbContext
{
    public CovidDbContext(DbContextOptions<CovidDbContext> options)
        : base(options) { }

    public DbSet<Country> Countries { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Add Fluent API configurations here if needed
        modelBuilder.Entity<Country>()
            .ToTable("countries") 
            .HasKey(c => c.CountryID);

        modelBuilder.Entity<Country>()
            .Property(c => c.CountryID)
            .HasColumnName("countryid"); 

        modelBuilder.Entity<Country>()
            .Property(c => c.CountryName)
            .HasColumnName("country_name");

        modelBuilder.Entity<Country>()
            .Property(c => c.Region)
            .HasColumnName("region");
            
        base.OnModelCreating(modelBuilder);
    }
}