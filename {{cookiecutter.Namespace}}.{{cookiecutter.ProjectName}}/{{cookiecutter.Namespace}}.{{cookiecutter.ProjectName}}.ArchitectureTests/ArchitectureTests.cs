using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using MediatR;
using NetArchTest.Rules;
using Space.Service.Common.EventBus;
using Space.Service.Common.Mediator.Mediator;
using {{cookiecutter.Namespace}}.{{cookiecutter.ProjectName}}.Api.Controllers;
using Xunit;

namespace {{cookiecutter.Namespace}}.{{cookiecutter.ProjectName}}.ArchitectureTests;

public class ArchitectureTests
{
    private const string apiLayer = "{{cookiecutter.Namespace}}.{{cookiecutter.ProjectName}}.Api";
    private const string applicationLayer = "{{cookiecutter.Namespace}}.{{cookiecutter.ProjectName}}.Application";
    private const string domainLayer = "{{cookiecutter.Namespace}}.{{cookiecutter.ProjectName}}.Domain";
    private const string infrastructureLayer = "{{cookiecutter.Namespace}}.{{cookiecutter.ProjectName}}.Infrastructure";
    private const string persistenceLayer = "{{cookiecutter.Namespace}}.{{cookiecutter.ProjectName}}.Persistence";

    private const string controllersNamespace = apiLayer + ".Controllers";
    private static readonly string mediatrNamespace = typeof(IMediator).Namespace!;

    public ArchitectureTests()
    {
        Assembly.Load(domainLayer);
        Assembly.Load(applicationLayer);
        Assembly.Load(apiLayer);
        Assembly.Load(infrastructureLayer);
        Assembly.Load(persistenceLayer);
    }

    [Fact]
    public void Controllers_ShouldNotDependOnPersistence()
    {
        TestResult result = ShouldNotDependOn(controllersNamespace, persistenceLayer);

        Assert.True(result.IsSuccessful, result.GetFailingTypeNames());
    }

    [Fact]
    public void Controllers_ShouldNotDependOnDomain()
    {
        TestResult result = ShouldNotDependOn(controllersNamespace, domainLayer);

        Assert.True(result.IsSuccessful, result.GetFailingTypeNames());
    }

    [Fact]
    public void Controllers_ShouldNotDependOnInfrastructure()
    {
        TestResult result = ShouldNotDependOn(controllersNamespace, infrastructureLayer);

        Assert.True(result.IsSuccessful, result.GetFailingTypeNames());
    }

    [Fact]
    public void Controllers_ShouldDependOnMediatr()
    {
        TestResult result = ShouldDependOn(controllersNamespace, mediatrNamespace);

        Assert.True(result.IsSuccessful, result.GetFailingTypeNames());
    }

    [Fact]
    public void Controllers_ShouldHaveNameEndingWithController()
    {
        TestResult testResult = Types.InNamespace(controllersNamespace)
            .That()
            .Inherit(typeof(ApiControllerBase))
            .Should()
            .HaveNameEndingWith("Controller")
            .GetResult();

        Assert.True(testResult.IsSuccessful, testResult.GetFailingTypeNames());
    }

    [Fact]
    public void Controllers_ShouldInheritFromBaseController()
    {
        TestResult testResult = Types.InNamespace(controllersNamespace)
            .That()
            .DoNotHaveName(typeof(ApiControllerBase).Name)
            .Should()
            .Inherit(typeof(ApiControllerBase))
            .GetResult();

        Assert.True(testResult.IsSuccessful, testResult.GetFailingTypeNames());
    }

    [Fact]
    public void Domain_ShouldNotDependOnAnyLayer()
    {
        TestResult result = ShouldNotDependOn(domainLayer, persistenceLayer,
            infrastructureLayer,
            apiLayer,
            applicationLayer);

        Assert.True(result.IsSuccessful, result.GetFailingTypeNames());
    }

    [Fact]
    public void Api_ShouldNotDependOnDomain()
    {
        TestResult result = ShouldNotDependOn(apiLayer, domainLayer);

        Assert.True(result.IsSuccessful, result.GetFailingTypeNames());
    }

    [Fact]
    public void Application_ShouldNotDependOnPersistence()
    {
        TestResult result = ShouldNotDependOn(applicationLayer, persistenceLayer);

        Assert.True(result.IsSuccessful, result.GetFailingTypeNames());
    }

    [Fact]
    public void Application_ShouldNotDependOnInfrastructure()
    {
        TestResult result = ShouldNotDependOn(applicationLayer, infrastructureLayer);

        Assert.True(result.IsSuccessful, result.GetFailingTypeNames());
    }

    [Fact]
    public void Requests_ShouldHaveNameEndingWithCommandOrQuery()
    {
        TestResult testResult = Types.InNamespace(applicationLayer)
            .That()
            .ImplementInterface(typeof(IRequest<>))
            .Should()
            .HaveNameEndingWith("Command")
            .Or()
            .HaveNameEndingWith("Query")
            .GetResult();

        Assert.True(testResult.IsSuccessful, testResult.GetFailingTypeNames());
    }

    [Fact]
    public void RequestHandlers_ShouldHaveNameEndingWithCommandHandlerOrQueryHandler()
    {
        TestResult testResult = Types.InNamespace(applicationLayer)
            .That()
            .Inherit(typeof(RequestHandlerBase<,>))
            .Should()
            .HaveNameEndingWith("CommandHandler")
            .Or()
            .HaveNameEndingWith("QueryHandler")
            .GetResult();

        Assert.True(testResult.IsSuccessful, testResult.GetFailingTypeNames());
    }

    [Fact]
    public void Repositories_ShouldHaveNameEndingWithRepository()
    {
        TestResult testResult = Types.InNamespace(persistenceLayer + ".Repositories")
            .Should()
            .HaveNameEndingWith("Repository")
            .Or()
            .BeGeneric()
            .GetResult();

        Assert.True(testResult.IsSuccessful, testResult.GetFailingTypeNames());
    }

    [Fact]
    public void RepositoryInterfaces_ShouldHaveNameEndingWithRepository()
    {
        TestResult testResult = Types.InNamespace(applicationLayer + ".Repositories")
            .Should()
            .HaveNameEndingWith("Repository")
            .Or()
            .BeGeneric()
            .GetResult();

        Assert.True(testResult.IsSuccessful, testResult.GetFailingTypeNames());
    }

    [Fact]
    public void ConsumedEvents_ShouldHaveNameEndingWithCommand()
    {
        TestResult testResult = Types.InNamespace(applicationLayer)
            .That()
            .HaveCustomAttribute(typeof(ConsumeEventAttribute))
            .Should()
            .HaveNameEndingWith("Command")
            .GetResult();

        Assert.True(testResult.IsSuccessful, testResult.GetFailingTypeNames());
    }

    [Fact]
    public void ProducedEvents_ShouldHaveNameEndingWithEvent()
    {
        TestResult testResult = Types.InNamespace(applicationLayer)
            .That()
            .HaveCustomAttribute(typeof(ProduceEventAttribute))
            .Should()
            .HaveNameEndingWith("Event")
            .GetResult();

        Assert.True(testResult.IsSuccessful, testResult.GetFailingTypeNames());
    }

    [Fact]
    public void ImportantClasses_ShouldNotBeDecoratedWith_ExcludeCodeCoverageAttribute()
    {
        ShouldNotExcludeCodeCoverage(apiLayer);
        ShouldNotExcludeCodeCoverage(applicationLayer);
        ShouldNotExcludeCodeCoverage(domainLayer);
        ShouldNotExcludeCodeCoverage(infrastructureLayer);
        ShouldNotExcludeCodeCoverage(persistenceLayer);
    }

    private static TestResult ShouldNotDependOn(string layer, params string[] dependencies)
    {
        return Types.InCurrentDomain()
            .That()
            .ResideInNamespace(layer)
            .ShouldNot()
            .HaveDependencyOnAny(dependencies)
            .GetResult();
    }

    private static TestResult ShouldDependOn(string layer, params string[] dependencies)
    {
        return Types.InCurrentDomain()
            .That()
            .ResideInNamespace(layer)
            .Should()
            .HaveDependencyOnAll(dependencies)
            .GetResult();
    }
    
    private static void ShouldNotExcludeCodeCoverage(string sourceNamespace)
    {
        bool testResult = Extensions.TypeShouldNotBeDecoratedWith(typeof(ExcludeFromCodeCoverageAttribute), sourceNamespace);
        Assert.True(testResult);
    }
}
