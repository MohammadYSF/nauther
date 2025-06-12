using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nauther.Framework.RestApi.Attributes;
using Nauther.Identity.Application.Features.Permission.Commands.CreatePermission;
using Nauther.Identity.Application.Features.Permission.Commands.De_etePermission;
using Nauther.Identity.Application.Features.Permission.Commands.EditPermission;
using Nauther.Identity.Application.Features.Permission.Queries.GetPermissionById;
using Nauther.Identity.Application.Features.Permission.Queries.GetPermissionByName;
using Nauther.Identity.Application.Features.Permission.Queries.GetPermissionList;

namespace Nauther.Identity.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
// [Authorize(AuthenticationSchemes = "Bearer")]
//[PermissionAuthorization("AccessAll")]
public class PermissionController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [PermissionAuthorization("ViewPermission")]
    [HttpGet("all")]
    public async Task<IActionResult> Get([FromQuery] GetPermissionsListQuery request)
    {
        var result = await _mediator.Send(request);
        return StatusCode(result.StatusCode, result);
    }

    // [PermissionAuthorization("GetPermissionByName")]
    [HttpGet("name")]
    public async Task<IActionResult> Get([FromQuery] GetPermissionByNameQuery request)
    {
        var result = await _mediator.Send(request);
        return StatusCode(result.StatusCode, result);
    }

    // [PermissionAuthorization("GetPermissionById")]
    [HttpGet]
    [Route("id")]
    public async Task<IActionResult> Get([FromQuery] GetPermissionByIdQuery request)
    {
        var result = await _mediator.Send(request);
        return StatusCode(result.StatusCode, result);
    }

    //[PermissionAuthorization("CreatePermission")]
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreatePermissionCommand request)
    {
        var result = await _mediator.Send(request);
        return StatusCode(result.StatusCode, result);
    }
    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> Post([FromRoute] Guid id, [FromBody] EditPermissionCommand request)
    {
        var result = await _mediator.Send(request);
        return StatusCode(result.StatusCode, result);
    }
    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody] DeletePermissionCommand request)
    {

        var result = await _mediator.Send(request);
        return StatusCode(result.StatusCode, result);
    }
}