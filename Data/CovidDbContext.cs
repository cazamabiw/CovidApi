using CovidApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CovidApi.Data;

public partial class CovidDbContext : DbContext
{
    public CovidDbContext(DbContextOptions<CovidDbContext> options) : base(options) { }

    public DbSet<CovidCase> CovidCases { get; set; }
    public DbSet<VaccinationData> VaccinationData { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CovidCase>(entity =>
        {
            entity.ToTable("covidcases");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DateReported).HasColumnName("date_reported");
            entity.Property(e => e.CountryCode).HasColumnName("country_code");
            entity.Property(e => e.Country).HasColumnName("country");
            entity.Property(e => e.WhoRegion).HasColumnName("who_region");
            entity.Property(e => e.NewCases).HasColumnName("new_cases");
            entity.Property(e => e.CumulativeCases).HasColumnName("cumulative_cases");
            entity.Property(e => e.NewDeaths).HasColumnName("new_deaths");
            entity.Property(e => e.CumulativeDeaths).HasColumnName("cumulative_deaths");
        });

        modelBuilder.Entity<VaccinationData>(entity =>
        {
            entity.ToTable("vaccinationdata");
            entity.HasKey(e => e.VaccineID);
            entity.Property(e => e.VaccineID).HasColumnName("vaccineid");
            entity.Property(e => e.Country).HasColumnName("country");
            entity.Property(e => e.Iso3).HasColumnName("iso3");
            entity.Property(e => e.WhoRegion).HasColumnName("who_region");
            entity.Property(e => e.DataSource).HasColumnName("data_source");
            entity.Property(e => e.DateUpdated).HasColumnName("date_updated");
            entity.Property(e => e.TotalVaccinations).HasColumnName("total_vaccinations");
            entity.Property(e => e.PersonsVaccinated1PlusDose).HasColumnName("persons_vaccinated_1plus_dose");
            entity.Property(e => e.TotalVaccinationsPer100).HasColumnName("total_vaccinations_per100");
            entity.Property(e => e.PersonsVaccinated1PlusDosePer100).HasColumnName("persons_vaccinated_1plus_dose_per100");
            entity.Property(e => e.PersonsLastDose).HasColumnName("persons_last_dose");
            entity.Property(e => e.PersonsLastDosePer100).HasColumnName("persons_last_dose_per100");
            entity.Property(e => e.VaccinesUsed).HasColumnName("vaccines_used");
            entity.Property(e => e.FirstVaccineDate).HasColumnName("first_vaccine_date");
            entity.Property(e => e.NumberVaccineTypesUsed).HasColumnName("number_vaccine_types_used");
            entity.Property(e => e.PersonsBoosterAddDose).HasColumnName("persons_booster_add_dose");
            entity.Property(e => e.PersonsBoosterAddDosePer100).HasColumnName("persons_booster_add_dose_per100");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("users");
            entity.HasKey(e => e.UserID);
            entity.Property(e => e.UserID).HasColumnName("userID");
            entity.Property(e => e.Username).HasColumnName("username");
            entity.Property(e => e.PasswordHash).HasColumnName("password_hash");
            entity.Property(e => e.Role).HasColumnName("role");
        });
    }
}
