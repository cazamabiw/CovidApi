using CovidApi.Data;

public class UnitOfWork : IUnitOfWork
{
    private readonly CovidDbContext _context;
    public ICovidCaseRepository CovidCases { get; private set; }
    public IVaccinationDataRepository VaccinationData { get; private set; }

    public UnitOfWork(CovidDbContext context)
    {
        _context = context;
        CovidCases = new CovidCaseRepository(context);
        VaccinationData = new VaccinationDataRepository(context);
    }

    public int Complete()
    {
        return _context.SaveChanges();
    }
}
