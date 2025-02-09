
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.Logging;
using OrgChart.Core.Interfaces;

namespace OrgChart.Core.Context;

public class OperationContext : IOperationContext
{
    private readonly ILogger<OperationContext> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public OperationContext(ILogger<OperationContext> logger, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public async Task<T> StartOperation<T>(Func<Task<T>> action, [CallerMemberName] string actionName = "")
    {
        ArgumentNullException.ThrowIfNull(action);
        ArgumentNullException.ThrowIfNull(actionName);

        var fullActionName = $"{GetClassName(action)}.{actionName}";
        var stopwatch = Stopwatch.StartNew();
        try
        {
            await _unitOfWork.BeginTransaction();
            var result = await action();
            await _unitOfWork.Commit();
            return result;
        }
        catch
        {
            await _unitOfWork.Rollback();
            throw;
        }
        finally
        {
            _unitOfWork.Dispose();
            stopwatch.Stop();
            _logger.LogInformation("{ActionName} executed in {ElapsedMilliseconds} ms",
                fullActionName, stopwatch.ElapsedMilliseconds);
        }
    }

    public async Task StartOperation(Func<Task> action, [CallerMemberName] string actionName = "")
    {
        ArgumentNullException.ThrowIfNull(action);
        ArgumentNullException.ThrowIfNull(actionName);

        var fullActionName = $"{GetClassName(action)}.{actionName}";
        var stopwatch = Stopwatch.StartNew();
        try
        {
            await _unitOfWork.BeginTransaction();
            await action();
            await _unitOfWork.Commit();
        }
        catch
        {
            await _unitOfWork.Rollback();
            throw;
        }
        finally
        {
            stopwatch.Stop();
            _logger.LogInformation("{ActionName} executed in {ElapsedMilliseconds} ms",
                fullActionName, stopwatch.ElapsedMilliseconds);
        }
    }

    private static string? GetClassName(Func<Task> action)
    {
        return (action?.Target?.GetType())?.DeclaringType?.FullName;
    }
}
