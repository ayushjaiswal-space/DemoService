using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using {{cookiecutter.Namespace}}.{{cookiecutter.ProjectName}}.Application.Resources;

namespace {{cookiecutter.Namespace}}.{{cookiecutter.ProjectName}}.UnitTests.Fixtures;

public class LocalizerFixture : IDisposable
{
    private readonly IServiceCollection serviceCollection;
    private readonly ServiceProvider serviceProvider;

    public LocalizerFixture()
    {
        serviceCollection = new ServiceCollection();
        serviceCollection.AddLogging();
        serviceCollection.AddLocalization();
        serviceProvider = serviceCollection.BuildServiceProvider();
    }

    public IStringLocalizer<SharedResources> GetLocalizer()
    {
        return serviceProvider.GetService<IStringLocalizer<SharedResources>>();
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected void Dispose(bool disposing)
    {
        if (disposing)
        {
            serviceProvider.Dispose();
        }
    }
}