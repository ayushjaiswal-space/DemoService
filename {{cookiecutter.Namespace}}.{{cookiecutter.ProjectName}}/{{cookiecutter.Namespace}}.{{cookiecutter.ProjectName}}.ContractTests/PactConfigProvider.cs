using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using PactNet;
using Xunit.Abstractions;

namespace {{cookiecutter.Namespace}}.{{cookiecutter.ProjectName}}.ContractTests;

public static class PactConfigProvider
{
    public static PactConfig GetPactConfig(ITestOutputHelper output)
    {
        JsonSerializerSettings settings = new()
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            DateTimeZoneHandling = DateTimeZoneHandling.Utc
        };

        settings.Converters.Add(new StringEnumConverter());

        JsonConvert.DefaultSettings = () => settings;

        return new PactConfig
        {
            PactDir = "../../../contracts/",
            LogLevel = PactLogLevel.Debug,
            Outputters = new[] { new XUnitOutput(output) },
            DefaultJsonSettings = settings
        };
    }
}