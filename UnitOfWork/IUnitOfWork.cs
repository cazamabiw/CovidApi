public interface IUnitOfWork
{
    ICovidCaseRepository CovidCases { get; }
    IVaccinationDataRepository VaccinationData { get; }
    int Complete();
}
