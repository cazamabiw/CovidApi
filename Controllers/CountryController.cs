using Microsoft.AspNetCore.Mvc;
using CovidApi.Data;
using Microsoft.EntityFrameworkCore;

namespace CovidApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CountryController : ControllerBase
{
    private readonly CovidDbContext _context;

    public CountryController(CovidDbContext context)
    {
        _context = context;
    }

    [HttpGet("countries")]
    public async Task<IActionResult> GetCountries()
    {
        var countries = await _context.Countries.ToListAsync();

        return Ok(countries);
    }
}
