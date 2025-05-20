using MediatR;
using Microsoft.AspNetCore.Mvc;
using Nauther.Framework.RestApi.Attributes;
using Nauther.Identity.Application.Features.UserPermission.Commands;
using Nauther.Identity.Application.Features.UserPermission.Commands.CreateUserPermissions;

namespace Nauther.Identity.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserPermissionController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;
    
    [PermissionAuthorization("CreateUserPermission")]
    [HttpPost]
    public async Task<IActionResult> Post(List<CreateUserPermissionDto> request)
    {
        var command = new CreateUserPermissionCommand()
        {
            CreateUserPermissionDtos = request
        };
        var result = await _mediator.Send(command);
        return StatusCode(result.StatusCode, result);
    }
}