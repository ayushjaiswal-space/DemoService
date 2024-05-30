using {{cookiecutter.Namespace}}.{{cookiecutter.ProjectName}}.Api;
using Xunit;

namespace {{cookiecutter.Namespace}}.{{cookiecutter.ProjectName}}.ComponentTests.Fixtures;

[CollectionDefinition("SharedFixtures")]
public class SharedFixtureCollection : ICollectionFixture<CustomWebApplicationFactory<Program>>,
    ICollectionFixture<WireMockServerFixture>
{
}