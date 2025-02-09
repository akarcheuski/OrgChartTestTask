using OrgChart.Core.Exceptions;
using OrgChart.Core.Interfaces;
using OrgChart.Core.Models;

namespace OrgChart.Core.Services;

public class EmployeeService
{
    private readonly IEmployeeRepository _repository;

    public EmployeeService(IEmployeeRepository repository)
    {
        _repository = repository;
    }

    public async Task<Employee> CreateEmployee(EmployeeDto employeeDto)
    {
        ArgumentNullException.ThrowIfNull(employeeDto);
        if (employeeDto.ManagerId.HasValue)
        {
            _ = await _repository.GetByIdOrDefault(employeeDto.ManagerId.Value)
                ?? throw new ManagerNotFoundException();

            var managerDepth = await _repository.GetHierarchyDepth(employeeDto.ManagerId.Value);
            if (managerDepth + 1 > Constants.Employees.MaxDepth)
                throw new HierarchyDepthException(Constants.Employees.MaxDepth);
        }

        var employee = new Employee
        {
            Name = employeeDto.Name,
            ManagerId = employeeDto.ManagerId
        };

        return await _repository.AddEmployee(employee);
    }

    public async Task<EmployeeWithCountDto> GetEmployeeWithSubordinateCount(int id)
    {
        var employee = await _repository.GetByIdOrDefault(id)
            ?? throw new EmployeeNotFoundException();

        var subCount = await _repository.GetSubordinateCount(id);
        return new EmployeeWithCountDto(employee.Id, employee.Name, employee.ManagerId, subCount);
    }

    public async Task UpdateEmployee(int id, EmployeeDto dto)
    {
        ArgumentNullException.ThrowIfNull(dto, nameof(dto));

        var employee = await _repository.GetByIdOrDefault(id)
            ?? throw new EmployeeNotFoundException();

        if (dto.ManagerId.HasValue && dto.ManagerId != employee.ManagerId)
        {
            _ = await _repository.GetByIdOrDefault(dto.ManagerId.Value)
                ?? throw new ManagerNotFoundException();

            if (await _repository.HasCycle(id, dto.ManagerId.Value))
                throw new ManagerCycleException();

            var newManagerDepth = await _repository.GetHierarchyDepth(dto.ManagerId.Value);

            if (newManagerDepth + 1 > Constants.Employees.MaxDepth)
                throw new HierarchyDepthException(Constants.Employees.MaxDepth);
        }

        employee.Name = dto.Name;
        employee.ManagerId = dto.ManagerId;
        await _repository.UpdateEmployee(employee);
    }

    public async Task DeleteEmployee(int id)
    {
        var employee = await _repository.GetByIdOrDefault(id)
            ?? throw new EmployeeNotFoundException();

        var subordinates = employee.Subordinates.ToList();
        if (subordinates.Any())
        {
            foreach (var subordinate in subordinates)
            {
                subordinate.ManagerId = employee.ManagerId;
                await _repository.UpdateEmployee(subordinate);
            }
        }
        await _repository.DeleteEmployee(employee);
    }
}
