using {{cookiecutter.Namespace}}.{{cookiecutter.ProjectName}}.Persistence;

namespace {{cookiecutter.Namespace}}.{{cookiecutter.ProjectName}}.ComponentTests;

public class {{cookiecutter.ProjectName}}DbInitializer
{
    public static void Initialize({{cookiecutter.ProjectName}}DbContext context)
    {
        // Put your seeding for testing here.
        context.ChangeTracker.Clear();
    }
}
