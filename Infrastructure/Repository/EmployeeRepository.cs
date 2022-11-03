using Core.DTO;
using Core.Entity;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Infrastructure.Repository;

public class EmployeeRepository : Repository<Employee>
{
    public EmployeeRepository(EmployeeContext context) : base(context)
    {
    }

    public async Task<List<Employee>> GetAll(EmployeeFilterDto? employeetFilter)
    {
        string queryString = "SELECT * FROM Employee";

        if (employeetFilter != null)
        {
            if (!string.IsNullOrEmpty(employeetFilter.EmployeeLastName))
                queryString += $" WHERE UPPER(EmployeeLastName) LIKE UPPER('%{employeetFilter.EmployeeLastName}%')";

            if (!string.IsNullOrEmpty(employeetFilter.EmployeePhone))
                queryString += $" WHERE UPPER(EmployeePhone) LIKE UPPER('%{employeetFilter.EmployeePhone}%')";
        }

        queryString += " ORDER BY HireDate";

        Console.WriteLine(queryString);

        var query = _context.Employees.FromSqlRaw(queryString);
        var data = await query.ToListAsync();

        return data;

    }
}

