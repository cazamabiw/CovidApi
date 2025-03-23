using System;

namespace CovidApi.Models;

public class CovidCase
{
    public int Id { get; set; }
    public DateTime DateReported { get; set; }
    public string? CountryCode { get; set; }
    public string? Country { get; set; }
    public string? WhoRegion { get; set; }
    public int? NewCases { get; set; }
    public int? CumulativeCases { get; set; }
    public int? NewDeaths { get; set; }
    public int? CumulativeDeaths { get; set; }
}