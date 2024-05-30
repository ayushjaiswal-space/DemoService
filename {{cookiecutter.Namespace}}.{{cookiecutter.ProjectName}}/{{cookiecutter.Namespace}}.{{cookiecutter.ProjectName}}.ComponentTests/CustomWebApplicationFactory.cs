using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using {{cookiecutter.Namespace}}.Common.Caching;
using {{cookiecutter.Namespace}}.Common.EventBus;
using {{cookiecutter.Namespace}}.{{cookiecutter.ProjectName}}.Persistence;

namespace {{cookiecutter.Namespace}}.{{cookiecutter.ProjectName}}.ComponentTests;

// https://learn.microsoft.com/en-us/aspnet/core/test/integration-tests
public class CustomWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram>
    where TProgram : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            ServiceDescriptor descriptor = services.SingleOrDefault(
                service => service.ServiceType == typeof(DbContextOptions<{{cookiecutter.ProjectName}}DbContext>));

            services.Remove(descriptor);

            services.AddDbContext<{{cookiecutter.ProjectName}}DbContext>(options =>
            {
                options.UseInMemoryDatabase("InMemoryDatabase");
            });

            ServiceProvider serviceProvider = services.BuildServiceProvider();
            using IServiceScope scope = serviceProvider.CreateScope();
            IServiceProvider scopedServices = scope.ServiceProvider;
            {{cookiecutter.ProjectName}}DbContext db = scopedServices.GetRequiredService<{{cookiecutter.ProjectName}}DbContext>();

            db.Database.EnsureCreated();
            {{cookiecutter.ProjectName}}DbInitializer.Initialize(db);

            services.Remove(services.SingleOrDefault(service => service.ServiceType == typeof(IAuthorizationHandler)));
            services.AddScoped<IAuthorizationHandler, AnonymousAuthorizationHandler>();

            IEventBus eventBus = Substitute.For<IEventBus>();
            eventBus.BeginTransaction().Returns(Substitute.For<IEventBusTransaction>());
            services.AddScoped(_ => eventBus);

            ISuperCache superCache = Substitute.For<ISuperCache>();
            services.AddScoped(_ => superCache);
        });

        IConfigurationRoot configuration = new ConfigurationBuilder()
            .AddJsonFile($"{AppDomain.CurrentDomain.BaseDirectory}/appsettings.json")
            .Build();

        builder.UseEnvironment("Local");
        builder.UseConfiguration(configuration);
    }
}