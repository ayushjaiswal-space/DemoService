using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Space.Service.Common.ContractsGenerator;
using Space.Service.Common.EventSchemaGenerator;

namespace {{cookiecutter.Namespace}}.{{cookiecutter.ProjectName}}.CITools;

[ExcludeFromCodeCoverage]
public class Program
{
    protected Program()
    {
    }

    private static async Task Main(string[] args)
    {
        string serviceName = "{{cookiecutter.Namespace}}.{{cookiecutter.ProjectName}}";
        List<string> projects = new() { "{{cookiecutter.Namespace}}.{{cookiecutter.ProjectName}}.Domain", "{{cookiecutter.Namespace}}.{{cookiecutter.ProjectName}}.Application", "{{cookiecutter.Namespace}}.{{cookiecutter.ProjectName}}.Infrastructure" };

        string command = args.Length > 0 ? args.First().ToLower() : string.Empty;

        switch (command)
        {
            case "generate-events-schema":
                EventsSchemaGenerator.GenerateEventSchemaJson(projects, serviceName);
                break;
            case "generate-contracts":
                await ContractsGenerator.GenerateContractsAsync(projects, serviceName);
                break;
        }
    }
}
