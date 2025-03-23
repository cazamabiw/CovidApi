using CovidApi.Data;
using CovidApi.DTOs;
using CovidApi.Models;

public class CovidCaseRepository : Repository<CovidCase>, ICovidCaseRepository
{
    public CovidCaseRepository(CovidDbContext context) : base(context) { }

    public IEnumerable<CovidCase> GetLatestCasesByCountry(string country)
    {
        return _context.CovidCases
            .Where(c => c.Country.ToLower() == country.ToLower())
            .OrderByDescending(c => c.DateReported)
            .Take(1)
            .ToList();
        
    }
}
