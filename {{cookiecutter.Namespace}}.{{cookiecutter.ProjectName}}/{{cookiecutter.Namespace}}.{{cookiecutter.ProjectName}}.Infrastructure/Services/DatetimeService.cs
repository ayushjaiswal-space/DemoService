using {{cookiecutter.Namespace}}.{{cookiecutter.ProjectName}}.Application.Services;
using System.Diagnostics.CodeAnalysis;

namespace {{cookiecutter.Namespace}}.{{cookiecutter.ProjectName}}.Infrastructure.Services;

[ExcludeFromCodeCoverage]
public class DatetimeService : IDatetimeService
{
    public DateTime GetCurrentUtcDateTime()
    {
        return DateTime.UtcNow;
    }
}