namespace OrgChart.Core.Exceptions;

public class HierarchyDepthException : Exception
{
    public HierarchyDepthException(int maxDepth) : base($"Hierarchy depth cannot exceed {maxDepth} levels.") { }
}
