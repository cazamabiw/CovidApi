using System;
using System.Collections.Generic;

namespace CovidApi.Models;

public partial class Country
{
    public int CountryId { get; set; } // Primary Key
    
    public string CountryCode { get; set; } = string.Empty; // ISO Country Code
    
    public string CountryName { get; set; } = string.Empty; 
    
    public string WhoRegion { get; set; } = string.Empty; // WHO Region

    public virtual ICollection<CovidCase> CovidCases { get; set; } = new List<CovidCase>();

    public virtual ICollection<VaccinationData> VaccinationData { get; set; } = new List<VaccinationData>();
}
