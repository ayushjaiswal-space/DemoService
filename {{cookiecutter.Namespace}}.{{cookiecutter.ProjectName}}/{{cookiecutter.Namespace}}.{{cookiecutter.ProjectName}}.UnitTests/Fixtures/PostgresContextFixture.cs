using Microsoft.EntityFrameworkCore;
using {{cookiecutter.Namespace}}.{{cookiecutter.ProjectName}}.Persistence;
using {{cookiecutter.Namespace}}.{{cookiecutter.ProjectName}}.UnitTests.Persistence;
using Testcontainers.PostgreSql;
using Xunit;

namespace {{cookiecutter.Namespace}}.{{cookiecutter.ProjectName}}.UnitTests.Fixtures;

public class PostgresContextFixture : IAsyncLifetime
{
    private readonly PostgreSqlContainer postgres = new PostgreSqlBuilder()
        .WithImage("postgres:15-alpine")
        .Build();

    public {{cookiecutter.ProjectName}}DbContext Context { get; private set; }

    public async Task InitializeAsync()
    {
        await postgres.StartAsync();

        DbContextOptions<{{cookiecutter.ProjectName}}DbContext> options = new DbContextOptionsBuilder<{{cookiecutter.ProjectName}}DbContext>()
            .EnableSensitiveDataLogging()
            .UseNpgsql(postgres.GetConnectionString())
            .Options;

        Context = new {{cookiecutter.ProjectName}}DbContext(options);

        await Context.Database.EnsureCreatedAsync();
        {{cookiecutter.ProjectName}}DbInitializer.Initialize(Context);
    }

    public Task DisposeAsync()
    {
        Context.Dispose();
        return postgres.DisposeAsync().AsTask();
    }
}