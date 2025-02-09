using OrgChart.Core.Interfaces;
using OrgChart.Core.Models;

namespace OrgChart.Infrastructure.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly OrgChartDbContext _context;
    public EmployeeRepository(OrgChartDbContext context)
    {
        _context = context;
    }

    public Task<Employee?> GetByIdOrDefault(int id)
    {
        throw new NotImplementedException();
    }

    public Task<List<Employee>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<Employee> AddEmployee(Employee employee)
    {
        throw new NotImplementedException();
    }

    public Task UpdateEmployee(Employee employee)
    {
        throw new NotImplementedException();
    }

    public Task DeleteEmployee(Employee employee)
    {
        throw new NotImplementedException();
    }

    public Task<int> GetSubordinateCount(int employeeId)
    {
        throw new NotImplementedException();
    }

    public Task<int> GetHierarchyDepth(int employeeId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> HasCycle(int employeeId, int newManagerId)
    {
        throw new NotImplementedException();
    }
}