using {{cookiecutter.Namespace}}.{{cookiecutter.ProjectName}}.Api;
using {{cookiecutter.Namespace}}.{{cookiecutter.ProjectName}}.Api.Controllers;
using {{cookiecutter.Namespace}}.{{cookiecutter.ProjectName}}.Application;
using {{cookiecutter.Namespace}}.{{cookiecutter.ProjectName}}.Infrastructure;
using {{cookiecutter.Namespace}}.{{cookiecutter.ProjectName}}.Infrastructure.Services;
using {{cookiecutter.Namespace}}.{{cookiecutter.ProjectName}}.Persistence;
using {{cookiecutter.Namespace}}.{{cookiecutter.ProjectName}}.Persistence.Repositories;

namespace {{cookiecutter.Namespace}}.{{cookiecutter.ProjectName}}.ArchitectureTests;

internal static class TypesExcludedFromCodeCoverage
{
    private static readonly IReadOnlyCollection<Type> types = new List<Type>()
    {
        typeof(ApiExtensions),
        typeof(InfrastructureExtensions),
        typeof(Program),
        typeof(ApiControllerBase),
        typeof(ApplicationExtensions),
        typeof(PersistenceExtensions),
        typeof({{cookiecutter.ProjectName}}DbContext),
        typeof(Seeder),
        typeof(RepositoryBase<,>),
        typeof(DatetimeService)
    };

    internal static bool IsExcluded(Type type)
    {
        return types.Contains(type);
    }
}
