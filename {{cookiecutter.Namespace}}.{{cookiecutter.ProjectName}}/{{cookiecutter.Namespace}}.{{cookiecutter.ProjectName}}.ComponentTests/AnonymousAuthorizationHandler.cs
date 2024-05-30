using Microsoft.AspNetCore.Authorization;

namespace {{cookiecutter.Namespace}}.{{cookiecutter.ProjectName}}.ComponentTests;

public class AnonymousAuthorizationHandler : IAuthorizationHandler
{
    public Task HandleAsync(AuthorizationHandlerContext context)
    {
        foreach (IAuthorizationRequirement requirement in context.PendingRequirements.ToList())
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}