using Asp.Versioning.ApiExplorer;
using Prometheus;
using Space.Service.Common.Auth;
using Space.Service.Common.EventBus;
using Space.Service.Common.HealthChecks;
using Space.Service.Common.Middlewares;
using Space.Service.Common.Misc.Utils;
using Space.Service.Common.Swagger;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace {{cookiecutter.Namespace}}.{{cookiecutter.ProjectName}}.Api;

[ExcludeFromCodeCoverage]
public static class ApiExtensions
{
    // Registration of services required by the presentation layer.
    public static IServiceCollection AddAPI(this IServiceCollection services, IConfiguration configuration)
    {
        IMvcBuilder mvcBuilder = services.AddControllers()
            .ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true);

        mvcBuilder.AddJson();

        services.AddIdentityServerAuthentication(configuration);
        services.AddLocalization();
        services.AddRouting(options => options.LowercaseUrls = true);
        services.AddHealthChecks(configuration);
        services.AddEndpointsApiExplorer();
        services.AddVersioning();
        services.AddSwagger();

        return services;
    }

    public static IApplicationBuilder ConfigureAPI(
        this IApplicationBuilder app,
        IWebHostEnvironment env,
        IConfiguration configuration,
        IApiVersionDescriptionProvider provider)
    {
        string pathBase = configuration["PATH_BASE"];

        app.UsePathBase(pathBase);
        app.UseLocalization();
        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseHttpMetrics();
        app.UseAuthentication();
        app.UseStaticFiles();
        app.UseAuthorization();
        app.UseMiddlewares();
        app.UseEventEndpoints();
        app.UseVersionEndpoint(configuration);
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapMetrics();
        });
        PrometheusUtils.SetPrometheusStaticLabels();
        app.UseHealthCheckEndpoints(env);
        app.UseSwagger(env, provider, pathBase);

        return app;
    }

    private static IApplicationBuilder UseEventEndpoints(this IApplicationBuilder app)
    {
        ParallelQuery<Type> types = AppDomain.CurrentDomain.GetAssemblies()
            .Where(assembly => !assembly.IsDynamic &&
                               !assembly.FullName.StartsWith("System") &&
                               !assembly.FullName.StartsWith("Microsoft"))
            .AsParallel()
            .SelectMany(assembly => assembly.GetTypes())
            .Where(type => type.GetCustomAttribute<ProduceEventAttribute>() != null);

        foreach (Type type in types)
        {
            ProduceEventAttribute produceEventAttribute = type.GetCustomAttribute<ProduceEventAttribute>();

            (app as IEndpointRouteBuilder)
                .MapGet($"/{produceEventAttribute.Topic}/{produceEventAttribute.EventType}", () => { })
                .Produces(StatusCodes.Status200OK, type.UnderlyingSystemType)
                .WithTags("__events__");
        }

        return app;
    }
}