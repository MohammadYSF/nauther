using MediatR;
using Microsoft.AspNetCore.Mvc;
using Nauther.Framework.RestApi.Attributes;
using Nauther.Identity.Application.Features.UserGroup.Commands.CreateUserRole;
using Nauther.Identity.Application.Features.UserRole.Commands.CreateUserRole;

namespace Nauther.Identity.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserGroupController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;
    
    [PermissionAuthorization("CreateUserGroup")]
    [HttpPost]
    public async Task<IActionResult> Post(List<CreateUserGroupDto> request)
    {
        var command = new CreateUserGroupCommand()
        {
            CreateUserGroupDtos = request
        };
        var result = await _mediator.Send(command);
        return StatusCode(result.StatusCode, result);
    }
}