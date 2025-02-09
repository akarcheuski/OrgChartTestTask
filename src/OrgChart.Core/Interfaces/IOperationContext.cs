using System.Runtime.CompilerServices;

namespace OrgChart.Core.Interfaces;

public interface IOperationContext
{
    /// <summary>
    /// Tracks the execution time of an asynchronous function that returns a result.
    /// </summary>
    /// <typeparam name="T">The type of the result returned by the function.</typeparam>
    /// <param name="action">The asynchronous function to track.</param>
    /// <param name="actionName">The name of the action being tracked. This is optional and will default to the caller member name.</param>
    /// <returns>A task that represents the asynchronous operation, containing the result of the function.</returns>
    Task<T> StartOperation<T>(Func<Task<T>> action, [CallerMemberName] string actionName = "");

    /// <summary>
    /// Tracks the execution time of an asynchronous function that does not return a result.
    /// </summary>
    /// <param name="action">The asynchronous function to track.</param>
    /// <param name="actionName">The name of the action being tracked. This is optional and will default to the caller member name.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task StartOperation(Func<Task> action, [CallerMemberName] string actionName = "");
}
