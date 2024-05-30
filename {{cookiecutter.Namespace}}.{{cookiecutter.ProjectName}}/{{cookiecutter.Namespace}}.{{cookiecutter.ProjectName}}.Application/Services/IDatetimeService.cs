namespace {{cookiecutter.Namespace}}.{{cookiecutter.ProjectName}}.Application.Services;

public interface IDatetimeService
{
    DateTime GetCurrentUtcDateTime();
}