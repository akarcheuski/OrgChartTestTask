using OrgChart.Core.Interfaces;
using OrgChart.Core.Models;
using OrgChart.Core.Services;

namespace OrgChart.Core.UseCases;

public class UpdateEmployeeUseCase
{
    private readonly EmployeeService _service;
    private readonly IOperationContext _operationContext;

    public UpdateEmployeeUseCase(EmployeeService service, IOperationContext operationContext)
    {
        _service = service;
        _operationContext = operationContext;
    }

    public async Task Execute(int id, EmployeeDto dto)
    {
        await _operationContext.StartOperation(() => _service.UpdateEmployee(id, dto));
    }
}
