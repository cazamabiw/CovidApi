using System.Collections.Generic;
using System.Linq;
using CovidApi.Models;
using CovidAPI.Interfaces;
using CovidApi.DTOs;
using System.Globalization;
namespace CovidAPI.Services
{
    public class CovidCaseService : ICovidCaseService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CovidCaseService> _logger;
        public CovidCaseService(IUnitOfWork unitOfWork, ILogger<CovidCaseService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public IEnumerable<CovidCaseDto> GetAllCases()
        {
            try
            {
                return _unitOfWork.CovidCases.GetAll()
                    .OrderByDescending(c => c.DateReported)
                    .Select(c => new CovidCaseDto
                    {
                        Id = c.Id,
                        DateReported = c.DateReported,
                        Country = c.Country,
                        CountryCode = c.CountryCode,
                        NewCases = c.NewCases,
                        CumulativeCases = c.CumulativeCases
                    }).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to get all COVID cases");
                throw;
            }
        }

        public CovidCaseDto GetCaseById(int id)
        {
            try
            {
                var c = _unitOfWork.CovidCases.GetById(id);
                if (c == null) return null;

                return new CovidCaseDto
                {
                    Id = c.Id,
                    DateReported = c.DateReported,
                    Country = c.Country,
                    CountryCode = c.CountryCode,
                    NewCases = c.NewCases,
                    CumulativeCases = c.CumulativeCases
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to get COVID case with ID {id}");
                throw;
            }
        }

        public void CreateCase(CovidCaseDto covidCaseDto)
        {
            try
            {
                var covidCaseEntity = new CovidCase
                {
                    DateReported = covidCaseDto.DateReported,
                    Country = covidCaseDto.Country,
                    CountryCode = covidCaseDto.CountryCode,
                    NewCases = covidCaseDto.NewCases,
                    CumulativeCases = covidCaseDto.CumulativeCases
                };

                _unitOfWork.CovidCases.Add(covidCaseEntity);
                _unitOfWork.Complete();

                covidCaseDto.Id = covidCaseEntity.Id;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to create new COVID case");
                throw;
            }
        }

        public void UpdateCase(CovidCaseDto covidCaseDto)
        {
            try
            {
                var existingCase = _unitOfWork.CovidCases.GetById(covidCaseDto.Id);
                if (existingCase == null) throw new Exception("Case not found");

                existingCase.DateReported = covidCaseDto.DateReported;
                existingCase.Country = covidCaseDto.Country;
                existingCase.CountryCode = covidCaseDto.CountryCode;
                existingCase.NewCases = covidCaseDto.NewCases;
                existingCase.CumulativeCases = covidCaseDto.CumulativeCases;

                _unitOfWork.CovidCases.Update(existingCase);
                _unitOfWork.Complete();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to update the COVID case");
                throw;
            }
        }

        public void DeleteCase(int id)
        {
            try
            {
                _unitOfWork.CovidCases.Delete(id);
                _unitOfWork.Complete();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to delete COVID case with ID {id}");
                throw;
            }
        }

        public IEnumerable<CovidCaseDto> GetLatestByCountry(string country)
        {
            try
            {
                var cases = _unitOfWork.CovidCases.GetLatestCasesByCountry(country);

                return cases.Select(c => new CovidCaseDto
                {
                    Id = c.Id,
                    DateReported = c.DateReported,
                    Country = c.Country,
                    CountryCode = c.CountryCode,
                    NewCases = c.NewCases,
                    CumulativeCases = c.CumulativeCases
                }).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to get latest COVID cases for country {country}");
                throw;
            }
        }
        public IEnumerable<ChartDataDto> GetGlobalWeeklyCases()
        {
            var culture = CultureInfo.InvariantCulture;
            var calendar = culture.Calendar;
            var rule = CalendarWeekRule.FirstFourDayWeek;
            var firstDayOfWeek = DayOfWeek.Monday;

            return _unitOfWork.CovidCases.GetAll()
                .Where(c => c.NewCases.HasValue)
                .AsEnumerable()
                .GroupBy(c =>
                {
                    var week = calendar.GetWeekOfYear(c.DateReported, rule, firstDayOfWeek);
                    return new { c.DateReported.Year, Week = week };
                })
                .Select(g => new ChartDataDto
                {
                    Label = $"W{g.Key.Week}-{g.Key.Year}",
                    Value = g.Sum(c => c.NewCases ?? 0)
                })
                .OrderBy(g => g.Label)
                .ToList();
        }

        public IEnumerable<ChartDataDto> GetTop10CountriesByCases()
        {
            return _unitOfWork.CovidCases.GetAll()
                .Where(c => c.CumulativeCases.HasValue)
                .GroupBy(c => c.Country)
                .Select(g => new ChartDataDto
                {
                    Label = g.Key,
                    Value = g.Max(c => c.CumulativeCases ?? 0)
                })
                .OrderByDescending(c => c.Value)
                .Take(10)
                .ToList();
        }

        public IEnumerable<ChartDataDto> GetCountryTrend(string country)
        {
            return _unitOfWork.CovidCases.GetAll()
                .Where(c => c.Country.ToLower() == country.ToLower())
                .OrderBy(c => c.DateReported)
                .Select(c => new ChartDataDto
                {
                    Label = c.DateReported.ToString("yyyy-MM-dd"),
                    Value = c.NewCases ?? 0
                })
                .ToList();
        }

    }
}
