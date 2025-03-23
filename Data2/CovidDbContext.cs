using Microsoft.EntityFrameworkCore;

namespace CovidApi.Models;

public partial class CovidDbContext : DbContext
{
    public CovidDbContext(DbContextOptions<CovidDbContext> options) : base(options) { }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<CovidCase> CovidCases { get; set; }

    public virtual DbSet<VaccinationData> VaccinationData { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Country>(entity =>
        {
            entity.ToTable("Countries");
            entity.HasKey(e => e.CountryId);

            entity.Property(e => e.CountryCode).HasMaxLength(10);
            entity.Property(e => e.CountryName).HasMaxLength(100);
            entity.Property(e => e.WhoRegion).HasMaxLength(50);
        });

        modelBuilder.Entity<CovidCase>(entity =>
        {
            entity.ToTable("CovidCases");
            entity.HasKey(e => e.Id);

            entity.Property(e => e.DateReported).HasColumnName("date_reported");

            entity.HasOne(d => d.Country)
                .WithMany(p => p.CovidCases)
                .HasForeignKey(d => d.CountryId);
        });

        modelBuilder.Entity<VaccinationData>(entity =>
        {
            entity.ToTable("VaccinationData");
            entity.HasKey(e => e.VaccineId);

            entity.HasOne(d => d.Country)
                .WithMany(p => p.VaccinationData)
                .HasForeignKey(d => d.CountryId);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("Users");
            entity.HasKey(e => e.UserId);
        });
    }
}
