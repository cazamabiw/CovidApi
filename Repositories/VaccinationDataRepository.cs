using CovidApi.Data;
using CovidApi.DTOs;
using CovidApi.Models;

public class VaccinationDataRepository : Repository<VaccinationData>, IVaccinationDataRepository
{
    public VaccinationDataRepository(CovidDbContext context) : base(context) { }

}
