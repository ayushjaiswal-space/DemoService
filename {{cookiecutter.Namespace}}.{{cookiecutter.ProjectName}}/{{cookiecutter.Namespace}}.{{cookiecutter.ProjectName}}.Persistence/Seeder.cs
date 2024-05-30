using System.Diagnostics.CodeAnalysis;

namespace {{cookiecutter.Namespace}}.{{cookiecutter.ProjectName}}.Persistence;

[ExcludeFromCodeCoverage]
public static class Seeder
{
    public static async Task Seed(this {{cookiecutter.ProjectName}}DbContext db)
    {
        // Put your seeding here.
        await Task.CompletedTask;
    }
}
