using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Diagnostics.CodeAnalysis;

namespace {{cookiecutter.Namespace}}.{{cookiecutter.ProjectName}}.Api.Controllers;

[Authorize]
[ApiController]
[Produces("application/json")]
[ExcludeFromCodeCoverage]
public abstract class ApiControllerBase : ControllerBase
{
    protected readonly IMediator mediator;

    protected ApiControllerBase(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [NonAction]
    protected ObjectResult OkResult([ActionResultObjectValue] object value)
        => new(value) { StatusCode = StatusCodes.Status200OK };

    [NonAction]
    protected ObjectResult CreatedResult([ActionResultObjectValue] object value)
        => new(value) { StatusCode = StatusCodes.Status201Created };
}
