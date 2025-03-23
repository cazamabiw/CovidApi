using System;

namespace CovidApi.DTOs;

public class VaccinationDataDto
{
    public string Country { get; set; }
    public long? TotalVaccinations { get; set; }
    public long? PersonsVaccinated1PlusDose { get; set; }
    public long? PersonsLastDose { get; set; }
    public long? PersonsBoosterAddDose { get; set; }
    public DateTime? DateUpdated { get; set; }
}
