namespace OrgChart.Core.Exceptions;

public class EmployeeNotFoundException : Exception
{
    public EmployeeNotFoundException() : base("Employee not found.") { }
}