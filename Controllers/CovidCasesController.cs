using CovidApi.DTOs;
using CovidApi.Models;
using CovidAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class CovidCasesController : ControllerBase
{
    private readonly ICovidCaseService _covidCaseService;

    public CovidCasesController(ICovidCaseService covidCaseService)
    {
        _covidCaseService = covidCaseService;
    }

    [HttpGet]
    public ActionResult<IEnumerable<CovidCaseDto>> GetAll()
    {
        var cases = _covidCaseService.GetAllCases();
        return Ok(cases);
    }

    [HttpGet("{id}")]
    public ActionResult<CovidCaseDto> GetById(int id)
    {
        var caseData = _covidCaseService.GetCaseById(id);
        if (caseData == null)
            return NotFound();
        return Ok(caseData);
    }

    [HttpGet("latest")]
    public ActionResult<IEnumerable<CovidCaseDto>> GetLatestByCountry([FromQuery] string country)
    {
        if (string.IsNullOrWhiteSpace(country))
            return BadRequest("Country is required.");

        var cases = _covidCaseService.GetLatestByCountry(country);

        if (!cases.Any())
            return NotFound($"No cases found for {country}");

        return Ok(cases);
    }

    [HttpPost]
    public IActionResult Create([FromBody] CovidCaseDto covidCase)
    {
        _covidCaseService.CreateCase(covidCase);
        return CreatedAtAction(nameof(GetById), new { id = covidCase.Id }, covidCase);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] CovidCaseDto updatedCase)
    {
        if (id != updatedCase.Id)
            return BadRequest();

        _covidCaseService.UpdateCase(updatedCase);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        _covidCaseService.DeleteCase(id);
        return NoContent();
    }
}
