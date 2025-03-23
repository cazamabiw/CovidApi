using System.Collections.Generic;
using System.Linq;
using CovidApi.Models;
using CovidAPI.Interfaces;
using CovidApi.DTOs;
using System.Globalization;
namespace CovidAPI.Services
{
    public class VaccinationDataService : IVaccinationDataService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<IVaccinationDataService> _logger;
        public VaccinationDataService(IUnitOfWork unitOfWork, ILogger<IVaccinationDataService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public IEnumerable<VaccinationDataDto> GetAllVaccinationData()
        {
            try
            {
                return _unitOfWork.VaccinationData.GetAll()
                    .OrderByDescending(v => v.DateUpdated)
                    .Select(v => new VaccinationDataDto
                    {
                        Country = v.Country,
                        TotalVaccinations = v.TotalVaccinations,
                        PersonsVaccinated1PlusDose = v.PersonsVaccinated1PlusDose,
                        PersonsLastDose = v.PersonsLastDose,
                        PersonsBoosterAddDose = v.PersonsBoosterAddDose,
                        DateUpdated = v.DateUpdated
                    })
                    .ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to get all Vaccinations");
                throw;
            }
        }

        public IEnumerable<ChartDataDto> GetTop10CountriesByTotalVaccinations()
        {
            try
            {
                return _unitOfWork.VaccinationData.GetAll()
                    .Where(v => v.TotalVaccinations.HasValue)
                    .GroupBy(v => v.Country)
                    .Select(g => new ChartDataDto
                    {
                        Label = g.Key,
                        Value = (int)(g.Max(v => v.TotalVaccinations ?? 0))
                    })
                    .OrderByDescending(x => x.Value)
                    .Take(10)
                    .ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to get Top 10 countries by total vaccinations");
                throw;
            }
        }

        public ChartDataDto GetLatestVaccinationForCountry(string country)
        {
            try
            {
                var latest = _unitOfWork.VaccinationData.GetAll()
                    .Where(v => v.Country.ToLower() == country.ToLower())
                    .OrderByDescending(v => v.DateUpdated)
                    .FirstOrDefault();

                if (latest == null) return null;

                return new ChartDataDto
                {
                    Label = latest.Country,
                    Value = (int)(latest.TotalVaccinations ?? 0)
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to get latest Vaccination for country {country}");
                throw;
            }
        }
    }
}
