using System;

namespace CovidApi.Models;

public partial class VaccinationData
{
    public int VaccineId { get; set; } // Primary Key

    public int CountryId { get; set; } // Foreign Key

    public string Iso3 { get; set; } = string.Empty; // ISO Code

    public string WhoRegion { get; set; } = string.Empty;

    public string DataSource { get; set; } = string.Empty;

    public DateOnly? DateUpdated { get; set; }

    public long? TotalVaccinations { get; set; }

    public long? PersonsVaccinated1PlusDose { get; set; }

    public int? TotalVaccinationsPer100 { get; set; }

    public int? PersonsVaccinated1PlusDosePer100 { get; set; }

    public long? PersonsLastDose { get; set; }

    public int? PersonsLastDosePer100 { get; set; }

    public string? VaccinesUsed { get; set; }

    public DateOnly? FirstVaccineDate { get; set; }

    public int? NumberVaccineTypesUsed { get; set; }

    public long? PersonsBoosterAddDose { get; set; }

    public int? PersonsBoosterAddDosePer100 { get; set; }

    public virtual Country Country { get; set; } = null!;
}
