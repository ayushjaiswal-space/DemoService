using Asp.Versioning.ApiExplorer;
using Space.Service.Common.Logging;
using Space.Service.Common.Misc.Utils;
using {{cookiecutter.Namespace}}.{{cookiecutter.ProjectName}}.Application;
using {{cookiecutter.Namespace}}.{{cookiecutter.ProjectName}}.Infrastructure;
using {{cookiecutter.Namespace}}.{{cookiecutter.ProjectName}}.Persistence;
using System.Diagnostics.CodeAnalysis;

namespace {{cookiecutter.Namespace}}.{{cookiecutter.ProjectName}}.Api;

[ExcludeFromCodeCoverage]
public class Program
{
    protected Program()
    {
    }

    private static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication
            .CreateBuilder(args);

        if (!builder.Environment.IsLocal())
        {
            builder.Configuration.AddJsonFile("/settings/globalsettings.json", false, true);
            builder.Configuration.AddJsonFile("/settings/appsettings.json", false, true);
        }

        builder.Host
            .UseSerilog(builder.Services, builder.Configuration)
            .UseDefaultServiceProvider((context, options) => { options.ValidateScopes = false; }) // Needed for Mediator DI.
            .UseApm(builder.Configuration);

        builder.Services
            .AddApplication()
            .AddPersistence(builder.Configuration)
            .AddInfrastructure(builder.Configuration)
            .AddAPI(builder.Configuration);

        builder.Services.ValidateServiceLifetimes();

        WebApplication app = builder.Build();

        {{cookiecutter.ProjectName}}DbContext db = app.Services.GetRequiredService<{{cookiecutter.ProjectName}}DbContext>();
        IWebHostEnvironment env = app.Services.GetRequiredService<IWebHostEnvironment>();
        IApiVersionDescriptionProvider provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

        app.ConfigurePersistence(db)
            .ConfigureAPI(env, builder.Configuration, provider);

        app.Run();
    }
}
