using Microsoft.EntityFrameworkCore;
using Space.Service.Common.Persistence;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace {{cookiecutter.Namespace}}.{{cookiecutter.ProjectName}}.Persistence;

[ExcludeFromCodeCoverage]
public class {{cookiecutter.ProjectName}}DbContext : DbContextBase
{
    public {{cookiecutter.ProjectName}}DbContext()
        : base(new DbContextOptionsBuilder<{{cookiecutter.ProjectName}}DbContext>().UseNpgsql("NpgSql").Options)
    {
    }

    public {{cookiecutter.ProjectName}}DbContext(DbContextOptions<{{cookiecutter.ProjectName}}DbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
