using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Space.Service.Common.Persistence;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace {{cookiecutter.Namespace}}.{{cookiecutter.ProjectName}}.Persistence;

[ExcludeFromCodeCoverage]
public static class PersistenceExtensions
{
    // Register your database, repositories and other database related services here.
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<{{cookiecutter.ProjectName}}DbContext>(options => options.UseNpgsql(configuration.GetConnectionString("NpgSql"), npgsqlOptions =>
        {
            npgsqlOptions.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
        }));

        return services;
    }

    // Configure you database here.
    public static IApplicationBuilder ConfigurePersistence(this IApplicationBuilder app, {{cookiecutter.ProjectName}}DbContext db)
    {
        if (db.Database.IsNpgsql())
        {
            // Create the database if it does not exist and run migrations
            db.Database.Create().Wait();
            db.Database.Migrate();
        }

        db.Seed().Wait();

        return app;
    }
}