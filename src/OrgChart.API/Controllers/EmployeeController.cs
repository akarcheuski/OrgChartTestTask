using Microsoft.AspNetCore.Mvc;
using OrgChart.Core.Models;
using OrgChart.Core.UseCases;

namespace OrgChart.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeeController : ControllerBase
{
    private readonly CreateEmployeeUseCase _createEmployeeUseCase;
    private readonly GetEmployeeUseCase _getEmployeeWithSubordinateCountUseCase;
    private readonly UpdateEmployeeUseCase _updateEmployeeUseCase;
    private readonly DeleteEmployeeUseCase _deleteEmployeeUseCase;

    public EmployeeController(
        CreateEmployeeUseCase createEmployeeUseCase,
        GetEmployeeUseCase getEmployeeWithSubordinateCountUseCase,
        UpdateEmployeeUseCase updateEmployeeUseCase,
        DeleteEmployeeUseCase deleteEmployeeUseCase)
    {
        _createEmployeeUseCase = createEmployeeUseCase;
        _getEmployeeWithSubordinateCountUseCase = getEmployeeWithSubordinateCountUseCase;
        _updateEmployeeUseCase = updateEmployeeUseCase;
        _deleteEmployeeUseCase = deleteEmployeeUseCase;
    }

    [HttpPost]
    public async Task<IActionResult> Create(EmployeeDto dto)
    {
        var employee = await _createEmployeeUseCase.Execute(dto);
        return CreatedAtAction(nameof(GetById), new { id = employee.Id }, employee);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _getEmployeeWithSubordinateCountUseCase.Execute(id);
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, EmployeeDto dto)
    {
        await _updateEmployeeUseCase.Execute(id, dto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _deleteEmployeeUseCase.Execute(id);
        return NoContent();
    }
}
