namespace OrgChart.Core.Exceptions;

public class ManagerCycleException : Exception
{
    public ManagerCycleException() : base("Cycle detected in hierarchy.") { }
}
