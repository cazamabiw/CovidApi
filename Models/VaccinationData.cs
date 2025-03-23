using System;

namespace CovidApi.Models;

public class VaccinationData
{
    public int VaccineID { get; set; }
    public string Country { get; set; }
    public string Iso3 { get; set; }
    public string? WhoRegion { get; set; }
    public string? DataSource { get; set; }
    public DateTime? DateUpdated { get; set; }
    public long? TotalVaccinations { get; set; }
    public long? PersonsVaccinated1PlusDose { get; set; }
    public int? TotalVaccinationsPer100 { get; set; }
    public int? PersonsVaccinated1PlusDosePer100 { get; set; }
    public long? PersonsLastDose { get; set; }
    public int? PersonsLastDosePer100 { get; set; }
    public string? VaccinesUsed { get; set; }
    public DateTime? FirstVaccineDate { get; set; }
    public int? NumberVaccineTypesUsed { get; set; }
    public long? PersonsBoosterAddDose { get; set; }
    public int? PersonsBoosterAddDosePer100 { get; set; }
}
