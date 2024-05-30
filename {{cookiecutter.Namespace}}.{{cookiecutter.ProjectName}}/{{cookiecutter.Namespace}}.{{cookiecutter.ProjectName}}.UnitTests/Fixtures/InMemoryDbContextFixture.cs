using Microsoft.EntityFrameworkCore;
using {{cookiecutter.Namespace}}.{{cookiecutter.ProjectName}}.Persistence;
using {{cookiecutter.Namespace}}.{{cookiecutter.ProjectName}}.UnitTests.Persistence;

namespace {{cookiecutter.Namespace}}.{{cookiecutter.ProjectName}}.UnitTests.Fixtures;

public class InMemoryDbContextFixture : IDisposable
{
    public InMemoryDbContextFixture()
    {
        DbContextOptions<{{cookiecutter.ProjectName}}DbContext> options = new DbContextOptionsBuilder<{{cookiecutter.ProjectName}}DbContext>()
            .EnableSensitiveDataLogging()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        Context = new {{cookiecutter.ProjectName}}DbContext(options);
        Context.Database.EnsureCreated();
        {{cookiecutter.ProjectName}}DbInitializer.Initialize(Context);
    }

    public {{cookiecutter.ProjectName}}DbContext Context { get; }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected void Dispose(bool disposing)
    {
        if (disposing)
        {
            Context.Dispose();
        }
    }
}
