using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Space.Service.Common.Mapping;
using Space.Service.Common.Mediator.Behaviors;
using Space.Service.Common.Misc;
using Space.Service.Common.Misc.Utils;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace {{cookiecutter.Namespace}}.{{cookiecutter.ProjectName}}.Application;

[ExcludeFromCodeCoverage]
public static class ApplicationExtensions
{
    // Register services required in Application layer here.
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<RequestMetadata>();
        services.AddMapping();
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            cfg.AddOpenBehavior(typeof(LoggingBehavior<,>));
            cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        ValidatorOptions.Global.DisplayNameResolver = (type, member, expression) => member?.Name.ToCamelCase();

        return services;
    }
}