using OrgChart.Core.Interfaces;
using OrgChart.Core.Models;
using OrgChart.Core.Services;

namespace OrgChart.Core.UseCases;

public class CreateEmployeeUseCase
{
    private readonly EmployeeService _service;
    private readonly IOperationContext _operationContext;

    public CreateEmployeeUseCase(EmployeeService service, IOperationContext operationContext)
    {
        _service = service;
        _operationContext = operationContext;
    }

    public async Task<Employee> Execute(EmployeeDto dto)
    {
        return await _operationContext.StartOperation(() => _service.CreateEmployee(dto));
    }
}
