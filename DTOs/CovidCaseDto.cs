using System;

namespace CovidApi.DTOs;
public class CovidCaseDto
{
    public int Id { get; set; }
    public DateTime DateReported { get; set; }
    public string Country { get; set; }
    public string CountryCode { get; set; }
    public int? NewCases { get; set; }
    public int? CumulativeCases { get; set; }
}
