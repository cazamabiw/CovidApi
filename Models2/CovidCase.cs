using System;

namespace CovidApi.Models;

public partial class CovidCase
{
    public int Id { get; set; }  // Renamed from CaseId to match DB schema
    
    public DateOnly DateReported { get; set; } // Renamed to match DB

    public int CountryId { get; set; } // Foreign Key

    public int? NewCases { get; set; }

    public int? CumulativeCases { get; set; } // Renamed from TotalCases

    public int? NewDeaths { get; set; }

    public int? CumulativeDeaths { get; set; } // Renamed from TotalDeaths

    public virtual Country Country { get; set; } = null!;
}
