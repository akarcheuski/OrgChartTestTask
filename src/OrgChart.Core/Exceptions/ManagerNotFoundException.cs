namespace OrgChart.Core.Exceptions;

public class ManagerNotFoundException : Exception
{
    public ManagerNotFoundException() : base("Manager not found.") { }
}
