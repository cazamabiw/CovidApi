using System.Collections.Generic;
using CovidApi.DTOs;
using CovidApi.Models;

namespace CovidAPI.Interfaces
{
    public interface ICovidCaseService
    {
        IEnumerable<CovidCaseDto> GetAllCases();
        CovidCaseDto GetCaseById(int id);
        IEnumerable<CovidCaseDto> GetLatestByCountry(string country);
        void CreateCase(CovidCaseDto covidCase);
        void UpdateCase(CovidCaseDto covidCase);
        void DeleteCase(int id);
        IEnumerable<ChartDataDto> GetGlobalWeeklyCases();
        IEnumerable<ChartDataDto> GetTop10CountriesByCases();
        IEnumerable<ChartDataDto> GetCountryTrend(string country);

    }
}
