using System;
using System.Collections.Generic;

namespace CovidApi.Models;

public partial class Vaccinationdatum
{
    public int Vaccineid { get; set; }

    public DateOnly? Date { get; set; }

    public int? Countryid { get; set; }

    public int? TotalVaccinated { get; set; }

    public int? NewVaccinated { get; set; }

    public string? VaccineType { get; set; }

    public virtual Country? Country { get; set; }
}
