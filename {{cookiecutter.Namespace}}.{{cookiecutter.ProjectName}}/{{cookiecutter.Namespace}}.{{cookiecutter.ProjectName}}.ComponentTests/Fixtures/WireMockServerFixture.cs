using WireMock.Server;

namespace {{cookiecutter.Namespace}}.{{cookiecutter.ProjectName}}.ComponentTests.Fixtures;

public class WireMockServerFixture : IDisposable
{
    private readonly WireMockServer wireMockServer;

    public WireMockServerFixture()
    {
        wireMockServer = WireMockServer.Start(port: 5980);
    }

    public WireMockServer GetWireMockServer()
    {
        return wireMockServer;
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
            wireMockServer.Dispose();
        }
    }
}