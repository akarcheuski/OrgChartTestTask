using OrgChart.Core.Interfaces;
using OrgChart.Core.Services;

namespace OrgChart.Core.UseCases;

public class DeleteEmployeeUseCase
{
    private readonly EmployeeService _service;
    private readonly IOperationContext _operationContext;

    public DeleteEmployeeUseCase(EmployeeService service, IOperationContext operationContext)
    {
        _service = service;
        _operationContext = operationContext;
    }

    public async Task Execute(int id)
    {
        await _operationContext.StartOperation(() => _service.DeleteEmployee(id));
    }
}
