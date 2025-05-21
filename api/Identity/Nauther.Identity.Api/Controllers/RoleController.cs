using MediatR;
using Microsoft.AspNetCore.Mvc;
using Nauther.Framework.RestApi.Attributes;
using Nauther.Identity.Application.Features.Role.Commands.CreateRole;
using Nauther.Identity.Application.Features.Role.Queries.GetRoleById;
using Nauther.Identity.Application.Features.Role.Queries.GetRoleByName;
using Nauther.Identity.Application.Features.Role.Queries.GetRolesList;

namespace Nauther.Identity.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
//[PermissionAuthorization("AccessAll")]
public class RoleController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    //[PermissionAuthorization("GetAllRoles")]
    [HttpGet("all")]
    public async Task<IActionResult> Get([FromQuery] GetRolesListQuery request)
    {
        var result = await _mediator.Send(request);
        return StatusCode(result.StatusCode, result);
    }

    [PermissionAuthorization("GetRoleByName")]
    [HttpGet("name")]
    public async Task<IActionResult> Get([FromQuery] GetRoleByNameQuery request)
    {
        var result = await _mediator.Send(request);
        return StatusCode(result.StatusCode, result);
    }

    [PermissionAuthorization("GetRoleById")]
    [HttpGet]
    [Route("id")]
    public async Task<IActionResult> Get([FromQuery] GetRoleByIdQuery request)
    {
        var result = await _mediator.Send(request);
        return StatusCode(result.StatusCode, result);
    }

    [PermissionAuthorization("CreateRole")]
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateRoleCommand request)
    {
        var result = await _mediator.Send(request);
        return StatusCode(result.StatusCode, result);
    }
}