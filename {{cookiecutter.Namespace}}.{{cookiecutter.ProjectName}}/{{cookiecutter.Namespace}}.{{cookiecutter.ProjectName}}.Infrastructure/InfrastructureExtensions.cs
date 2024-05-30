using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Space.Service.Common.EventBus;
using {{cookiecutter.Namespace}}.{{cookiecutter.ProjectName}}.Application.Services;
using {{cookiecutter.Namespace}}.{{cookiecutter.ProjectName}}.Infrastructure.Services;
using {{cookiecutter.Namespace}}.{{cookiecutter.ProjectName}}.Persistence;
using System.Diagnostics.CodeAnalysis;

namespace {{cookiecutter.Namespace}}.{{cookiecutter.ProjectName}}.Infrastructure;

[ExcludeFromCodeCoverage]
public static class InfrastructureExtensions
{
    // Register all your infrastructure services (REST API integrations, cache, etc.) here (except for database).
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMemoryCache();
        services.AddHttpClients(configuration);
        services.AddSingleton<IDatetimeService, DatetimeService>();
        services.AddEventBus(configuration, typeof({{cookiecutter.ProjectName}}DbContext));

        return services;
    }

    private static IServiceCollection AddHttpClients(this IServiceCollection services, IConfiguration configuration)
    {
        return services;
    }
}
