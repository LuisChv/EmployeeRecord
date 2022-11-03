using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Infrastructure.Repository;

public class Repository<Tentity> : IRepository<Tentity> where Tentity : class
{
    public readonly EmployeeContext _context;
    public readonly DbSet<Tentity> _dbSet;

    public Repository(EmployeeContext context)
    {
        _context = context;
        _dbSet = _context.Set<Tentity>();
    }

    public async Task<Tentity> Add(Tentity data) => (await _dbSet.AddAsync(data)).Entity;

    public async void AddRange(IEnumerable<Tentity> data)
    {
        await _dbSet.AddRangeAsync(data);
    }

    public async Task<Tentity> Get(Guid id)
    {
        var result = await _dbSet.FromSqlRaw($"SELECT * FROM Employee WHERE EmployeeID = '{id}'").FirstOrDefaultAsync();
        if (result != null)
            _context.Entry(result).State = EntityState.Detached;

        return result!;
    }

    public void Update(Tentity data)
    {
        _dbSet.Attach(data);
        _context.Entry(data).State = EntityState.Modified;
    }
    public async Task Save() => await _context.SaveChangesAsync();

    public async Task<IEnumerable<Tentity>> Get() => await _dbSet.FromSqlRaw($"SELECT * FROM Employe").ToListAsync();

    public async Task<bool> Delete(Guid id)
    {
        var dataToDelete = await _dbSet.FindAsync(id);
        if (dataToDelete == null) return false;
        _dbSet.Remove(dataToDelete);
        return true;
    }
}

