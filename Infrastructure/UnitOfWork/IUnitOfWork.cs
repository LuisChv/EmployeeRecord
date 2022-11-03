using Infrastructure.Repository;

namespace Infrastructure.UnitOfWork;
public interface IUnitOfWork
{
    public EmployeeRepository Employee { get; }

    public Task Save();
}
