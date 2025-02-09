using OrgChart.Core.Interfaces;
using OrgChart.Core.Services;

namespace OrgChart.Core.UseCases;

public class GetEmployeeUseCase
{
    private readonly EmployeeService _service;
    private readonly IOperationContext _operationContext;

    public GetEmployeeUseCase(EmployeeService service, IOperationContext operationContext)
    {
        _service = service;
        _operationContext = operationContext;
    }

    public async Task<EmployeeWithCountDto> Execute(int id)
    {
        return await _operationContext.StartOperation(() => _service.GetEmployeeWithSubordinateCount(id));
    }
}
