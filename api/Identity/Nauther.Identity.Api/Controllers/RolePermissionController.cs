using MediatR;
using Microsoft.AspNetCore.Mvc;
using Nauther.Framework.RestApi.Attributes;
using Nauther.Identity.Application.Features.RolePermission.Commands;
using Nauther.Identity.Application.Features.RolePermission.Commands.CreateRolePermissions;
using Nauther.Identity.Application.Features.UserRole.Commands.CreateUserRole;

namespace Nauther.Identity.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RolePermissionController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;
    
    [PermissionAuthorization("CreateRolePermission")]
    [HttpPost]
    public async Task<IActionResult> Post(List<CreateRolePermissionDto> request)
    {
        var command = new CreateRolePermissionCommand()
        {
            CreateRolePermissionDtos = request
        };
        var result = await _mediator.Send(command);
        return StatusCode(result.StatusCode, result);
    }
}