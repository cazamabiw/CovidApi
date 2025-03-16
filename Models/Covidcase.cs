using System;
using System.Collections.Generic;

namespace CovidApi.Models;

public partial class Covidcase
{
    public int Caseid { get; set; }

    public DateOnly Date { get; set; }

    public int? Countryid { get; set; }

    public int? NewCases { get; set; }

    public int? TotalCases { get; set; }

    public int? NewDeaths { get; set; }

    public int? TotalDeaths { get; set; }

    public int? NewRecoveries { get; set; }

    public int? TotalRecoveries { get; set; }

    public virtual Country? Country { get; set; }
}
