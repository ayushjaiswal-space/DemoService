using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Space.Service.Common.Mapping;

namespace {{cookiecutter.Namespace}}.{{cookiecutter.ProjectName}}.UnitTests.Fixtures;

public class MapperFixture : IDisposable
{
    private readonly IServiceCollection serviceCollection;
    private readonly ServiceProvider serviceProvider;

    public MapperFixture()
    {
        serviceCollection = new ServiceCollection();
        serviceCollection.AddMapping();
        serviceProvider = serviceCollection.BuildServiceProvider();
    }

    public IMapper GetMapper()
    {
        return serviceProvider.GetService<IMapper>();
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