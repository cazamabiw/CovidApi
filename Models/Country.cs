using System;
using System.Collections.Generic;

namespace CovidApi.Models;

public partial class Country
{
    public int Countryid { get; set; }

    public string? CountryName { get; set; }

    public string? Region { get; set; }

    public virtual ICollection<Covidcase> Covidcases { get; set; } = new List<Covidcase>();

    public virtual ICollection<Vaccinationdatum> Vaccinationdata { get; set; } = new List<Vaccinationdatum>();
}
