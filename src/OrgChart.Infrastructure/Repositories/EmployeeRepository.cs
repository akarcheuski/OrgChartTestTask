using System.Linq;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using OrgChart.Core.Interfaces;
using OrgChart.Core.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace OrgChart.Infrastructure.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly OrgChartDbContext _context;
    public EmployeeRepository(OrgChartDbContext context)
    {
        _context = context;
    }

    public async Task<Employee?> GetByIdOrDefault(int id)
    => await _context.Employees.AsNoTracking()
            .Where(e => e.Id == id).FirstOrDefaultAsync();

    public async Task<List<Employee>> GetAll()
   => await _context.Set<Employee>().ToListAsync();

    public async Task<Employee> AddEmployee(Employee employee)
    {
        await _context.AddAsync(employee);
        await _context.SaveChangesAsync();
        return employee;
    }

    public async Task UpdateEmployee(Employee employee)
    {
        var dbValue = await GetByIdOrDefault(employee.Id);
        _context.Set<Employee>().Attach(employee);
        _context.Entry(employee).State = EntityState.Modified;
        if (dbValue != null)
        {
            dbValue.Name = employee.Name;
            dbValue.Subordinates = employee.Subordinates;
            dbValue.Manager = employee.Manager;
            dbValue.ManagerId = employee.ManagerId;
        }
        await _context.SaveChangesAsync();
    }

    public async Task DeleteEmployee(Employee employee)
    {
        _context.Set<Employee>().Remove(employee);
        await _context.SaveChangesAsync();
    }

    public async Task<int> GetSubordinateCount(int employeeId)
    => await _context.Employees.CountAsync(e => e.ManagerId == employeeId);

    public async Task<int> GetHierarchyDepth(int employeeId)
    {
        var employee = await GetByIdOrDefault(employeeId);
        return await CalcDepth(employee);
    }

    public async Task<bool> HasCycle(int employeeId, int newManagerId)
    {
        var employee = await GetByIdOrDefault(employeeId);
        return employee.Subordinates.Any(s => s.Id == newManagerId);
    }

    public async Task<int> CalcDepth(Employee? employee, int depth = 1)
    {
        foreach (var subordinate in employee.Subordinates)
            await CalcDepth(subordinate, depth + 1);
        return depth;
    }
}