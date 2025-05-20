using MediatR;
using Microsoft.AspNetCore.Mvc;
using Nauther.Framework.RestApi.Attributes;
using Nauther.Identity.Application.Features.GroupPermission.Commands.CreateGroupPermissions;
using Nauther.Identity.Application.Features.RolePermission.Commands.CreateRolePermissions;

namespace Nauther.Identity.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GroupPermissionController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;
    
    [PermissionAuthorization("CreateGroupPermission")]
    [HttpPost]
    public async Task<IActionResult> Post(List<CreateGroupPermissionDto> request)
    {
        var command = new CreateGroupPermissionCommand()
        {
            CreateGroupPermissionDtos = request
        };
        var result = await _mediator.Send(command);
        return StatusCode(result.StatusCode, result);
    }
}