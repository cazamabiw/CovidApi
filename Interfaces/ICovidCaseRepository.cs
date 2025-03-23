using CovidApi.DTOs;
using CovidApi.Models;

public interface ICovidCaseRepository : IRepository<CovidCase>
{
    IEnumerable<CovidCase> GetLatestCasesByCountry(string country);
}

