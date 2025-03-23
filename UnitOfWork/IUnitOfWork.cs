public interface IUnitOfWork
{
    ICovidCaseRepository CovidCases { get; }
    int Complete();
}
