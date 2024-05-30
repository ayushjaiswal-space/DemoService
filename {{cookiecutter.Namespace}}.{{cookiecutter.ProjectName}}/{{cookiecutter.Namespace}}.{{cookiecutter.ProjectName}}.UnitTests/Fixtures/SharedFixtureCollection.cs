using Xunit;

namespace {{cookiecutter.Namespace}}.{{cookiecutter.ProjectName}}.UnitTests.Fixtures;

// If you need some shared objects in your test classes, you can create a separate class (for example MyClass)
// and inherit SharedFixtureCollection from ICollectionFixture<MyClass>,
// then you will be able to inject MyClass in your test class' constructor.
// The test class must have attribute [Collection("SharedFixtures")].
[CollectionDefinition("SharedFixtures")]
public class SharedFixtureCollection : ICollectionFixture<InMemoryDbContextFixture>,
    ICollectionFixture<MapperFixture>,
    ICollectionFixture<LocalizerFixture>,
    ICollectionFixture<PostgresContextFixture>
{
}
