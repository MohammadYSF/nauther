using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nauther.Framework.RestApi.Attributes;
using Nauther.Identity.Application.Features.Role.Commands.CreateRole;
using Nauther.Identity.Application.Features.Role.Commands.DeleteRole;
using Nauther.Identity.Application.Features.Role.Commands.EditRole;
using Nauther.Identity.Application.Features.Role.Queries.GetRoleById;
using Nauther.Identity.Application.Features.Role.Queries.GetRoleByName;
using Nauther.Identity.Application.Features.Role.Queries.GetRolesList;

namespace Nauther.Identity.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
// [Authorize(AuthenticationSchemes = "Bearer")]

//[PermissionAuthorization("AccessAll")]
public class RoleController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    //[PermissionAuthorization("GetAllRoles")]
    public async Task<IActionResult> Get([FromQuery] GetRolesListQuery request)
    {
        var result = await _mediator.Send(request);
        return StatusCode(result.StatusCode, result);
    }

    // [PermissionAuthorization("GetRoleByName")]
    [HttpGet("name")]
    public async Task<IActionResult> Get([FromQuery] GetRoleByNameQuery request)
    {
        var result = await _mediator.Send(request);
        return StatusCode(result.StatusCode, result);
    }

    //[PermissionAuthorization("GetRoleById")]
    [HttpGet]
    [Route("{Id}")]
    public async Task<IActionResult> Get(GetRoleByIdQuery request)
    {
        var result = await _mediator.Send(request);
        return StatusCode(result.StatusCode, result);
    }

    //[PermissionAuthorization("CreateRole")]
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateRoleCommand request)
    {
        var result = await _mediator.Send(request);
        return StatusCode(result.StatusCode, result);
    }
    [HttpPut]
    [Route("{Id}")]
    //[PermissionAuthorization("EditRole")]
    public async Task<IActionResult> Put([FromRoute] Guid Id,[FromBody] EditRoleCommand request)
    {
        request.Id = Id;
        var result = await _mediator.Send(request);
        return StatusCode(result.StatusCode, result);
    }
    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody] DeleteRoleCommand request)
    {
        var result = await _mediator.Send(request);
        return StatusCode(result.StatusCode, result);
    }
}