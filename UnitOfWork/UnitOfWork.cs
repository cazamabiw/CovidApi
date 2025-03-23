using CovidApi.Data;

public class UnitOfWork : IUnitOfWork
{
    private readonly CovidDbContext _context;
    public ICovidCaseRepository CovidCases { get; private set; }

    public UnitOfWork(CovidDbContext context)
    {
        _context = context;
        CovidCases = new CovidCaseRepository(context);
    }

    public int Complete()
    {
        return _context.SaveChanges();
    }
}
