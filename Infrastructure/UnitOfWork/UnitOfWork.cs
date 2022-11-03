using Infrastructure.Repository;
using Persistence;

namespace Infrastructure.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    public EmployeeRepository? _employees;

    private readonly EmployeeContext _context;

    public UnitOfWork(EmployeeContext context)
    {
        _context = context;
    }

    public EmployeeRepository Employee
    {
        get
        {
            return _employees == null ? _employees = new EmployeeRepository(_context) : _employees;
        }
    }

    public async Task Save()
    {
        await _context.SaveChangesAsync();
    }
}