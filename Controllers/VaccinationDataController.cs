using CovidApi.DTOs;
using CovidApi.Models;
using CovidAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class VaccinationDataController : ControllerBase
{
private readonly IVaccinationDataService _vaccinationService;

public VaccinationDataController(IVaccinationDataService vaccinationService)
{
    _vaccinationService = vaccinationService;
}

    [HttpGet("top-vaccinations")]
    public ActionResult<IEnumerable<ChartDataDto>> GetTop10CountriesByTotalVaccinations()
    {
       return Ok(_vaccinationService.GetTop10CountriesByTotalVaccinations());
    }

    [HttpGet("trend")]
    public ActionResult<IEnumerable<ChartDataDto>> GetLatestVaccinationForCountry([FromQuery] string country)
    {
        if (string.IsNullOrWhiteSpace(country))
            return BadRequest("Country name is required");

        return Ok(_vaccinationService.GetLatestVaccinationForCountry(country));
    }
}
