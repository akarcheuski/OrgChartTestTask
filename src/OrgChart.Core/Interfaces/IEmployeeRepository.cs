using OrgChart.Core.Models;

namespace OrgChart.Core.Interfaces;

public interface IEmployeeRepository
{
    /// <summary>
    /// Gets an employee by their ID or returns null if not found.
    /// </summary>
    /// <param name="id">The ID of the employee.</param>
    /// <returns>The employee with the specified ID or null.</returns>
    Task<Employee?> GetByIdOrDefault(int id);

    /// <summary>
    /// Gets all employees.
    /// </summary>
    /// <returns>A list of all employees.</returns>
    Task<List<Employee>> GetAll();

    /// <summary>
    /// Adds a new employee.
    /// </summary>
    /// <param name="employee">The employee to add.</param>
    /// <returns>The added employee.</returns>
    Task<Employee> AddEmployee(Employee employee);

    /// <summary>
    /// Updates an existing employee.
    /// </summary>
    /// <param name="employee">The employee to update.</param>
    Task UpdateEmployee(Employee employee);

    /// <summary>
    /// Deletes an employee.
    /// </summary>
    /// <param name="employee">The employee to delete.</param>
    Task DeleteEmployee(Employee employee);

    /// <summary>
    /// Gets the count of subordinates for a given employee.
    /// </summary>
    /// <param name="employeeId">The ID of the employee.</param>
    /// <returns>The count of subordinates.</returns>
    Task<int> GetSubordinateCount(int employeeId);

    /// <summary>
    /// Gets the depth of the hierarchy for a given employee.
    /// </summary>
    /// <param name="employeeId">The ID of the employee.</param>
    /// <returns>The depth of the hierarchy.</returns>
    Task<int> GetHierarchyDepth(int employeeId);

    /// <summary>
    /// Checks if adding a new manager would create a cycle in the hierarchy.
    /// </summary>
    /// <param name="employeeId">The ID of the employee.</param>
    /// <param name="newManagerId">The ID of the new manager.</param>
    /// <returns>True if a cycle would be created, otherwise false.</returns>
    Task<bool> HasCycle(int employeeId, int newManagerId);
}
