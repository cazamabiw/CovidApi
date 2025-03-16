using System;
using System.Collections.Generic;
using CovidApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CovidApi.Data;

public partial class CovidDbContext : DbContext
{
    public CovidDbContext()
    {
    }

    public CovidDbContext(DbContextOptions<CovidDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<Covidcase> Covidcases { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Vaccinationdatum> Vaccinationdata { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=host.docker.internal;Port=5432;Database=CovidReportSystem;Username=postgres;Password=123456");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.Countryid).HasName("countries_pkey");

            entity.ToTable("countries");

            entity.Property(e => e.Countryid).HasColumnName("countryid");
            entity.Property(e => e.CountryName)
                .HasMaxLength(100)
                .HasColumnName("country_name");
            entity.Property(e => e.Region)
                .HasMaxLength(100)
                .HasColumnName("region");
        });

        modelBuilder.Entity<Covidcase>(entity =>
        {
            entity.HasKey(e => e.Caseid).HasName("covidcases_pkey");

            entity.ToTable("covidcases");

            entity.Property(e => e.Caseid).HasColumnName("caseid");
            entity.Property(e => e.Countryid).HasColumnName("countryid");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.NewCases).HasColumnName("new_cases");
            entity.Property(e => e.NewDeaths).HasColumnName("new_deaths");
            entity.Property(e => e.NewRecoveries).HasColumnName("new_recoveries");
            entity.Property(e => e.TotalCases).HasColumnName("total_cases");
            entity.Property(e => e.TotalDeaths).HasColumnName("total_deaths");
            entity.Property(e => e.TotalRecoveries).HasColumnName("total_recoveries");

            entity.HasOne(d => d.Country).WithMany(p => p.Covidcases)
                .HasForeignKey(d => d.Countryid)
                .HasConstraintName("covidcases_countryid_fkey");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Userid).HasName("users_pkey");

            entity.ToTable("users");

            entity.Property(e => e.Userid).HasColumnName("userid");
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(255)
                .HasColumnName("password_hash");
            entity.Property(e => e.Role)
                .HasMaxLength(50)
                .HasColumnName("role");
            entity.Property(e => e.Username)
                .HasMaxLength(100)
                .HasColumnName("username");
        });

        modelBuilder.Entity<Vaccinationdatum>(entity =>
        {
            entity.HasKey(e => e.Vaccineid).HasName("vaccinationdata_pkey");

            entity.ToTable("vaccinationdata");

            entity.Property(e => e.Vaccineid).HasColumnName("vaccineid");
            entity.Property(e => e.Countryid).HasColumnName("countryid");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.NewVaccinated).HasColumnName("new_vaccinated");
            entity.Property(e => e.TotalVaccinated).HasColumnName("total_vaccinated");
            entity.Property(e => e.VaccineType)
                .HasMaxLength(100)
                .HasColumnName("vaccine_type");

            entity.HasOne(d => d.Country).WithMany(p => p.Vaccinationdata)
                .HasForeignKey(d => d.Countryid)
                .HasConstraintName("vaccinationdata_countryid_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
