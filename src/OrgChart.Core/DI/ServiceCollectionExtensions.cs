using Microsoft.Extensions.DependencyInjection;
using OrgChart.Core.Context;
using OrgChart.Core.Interfaces;
using OrgChart.Core.Services;
using OrgChart.Core.UseCases;

namespace OrgChart.Core.DI;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCoreServices(this IServiceCollection services)
    {
        services.AddScoped<EmployeeService>();
        // services.AddScoped<EmployeeImplementedService>();
        services.AddScoped<CreateEmployeeUseCase>();
        services.AddScoped<GetEmployeeUseCase>();
        services.AddScoped<UpdateEmployeeUseCase>();
        services.AddScoped<DeleteEmployeeUseCase>();
        services.AddScoped<IOperationContext, OperationContext>();

        return services;
    }
}
