using MediatR;
using Microsoft.AspNetCore.Mvc;
using Nauther.Framework.RestApi.Attributes;
using Nauther.Identity.Application.Features.UserRole.Commands.CreateUserRole;

namespace Nauther.Identity.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserRoleController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;
    
    [PermissionAuthorization("CreateUserRole")]
    [HttpPost]
    public async Task<IActionResult> Post(List<CreateUserRoleDto> request)
    {
        var command = new CreateUserRoleCommand()
        {
            CreateUserRoleDtos = request
        };
        var result = await _mediator.Send(command);
        return StatusCode(result.StatusCode, result);
    }
}