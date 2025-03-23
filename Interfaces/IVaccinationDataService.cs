using System.Collections.Generic;
using CovidApi.DTOs;
using CovidApi.Models;

namespace CovidAPI.Interfaces
{
    public interface IVaccinationDataService
    {
        IEnumerable<VaccinationDataDto> GetAllVaccinationData();

        IEnumerable<ChartDataDto> GetTop10CountriesByTotalVaccinations();
        ChartDataDto GetLatestVaccinationForCountry(string country);

    }
}
