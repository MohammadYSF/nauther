using MediatR;
using Microsoft.AspNetCore.Mvc;
using Nauther.Framework.RestApi.Attributes;
using Nauther.Identity.Application.Features.Permission.Commands.CreatePermission;
using Nauther.Identity.Application.Features.Permission.Queries.GetPermissionById;
using Nauther.Identity.Application.Features.Permission.Queries.GetPermissionByName;
using Nauther.Identity.Application.Features.Permission.Queries.GetPermissionList;

namespace Nauther.Identity.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
//[PermissionAuthorization("AccessAll")]
public class PermissionController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [PermissionAuthorization("GetAllPermissions")]
    [HttpGet("all")]
    public async Task<IActionResult> Get([FromQuery] GetPermissionsListQuery request)
    {
        var result = await _mediator.Send(request);
        return StatusCode(result.StatusCode, result);
    }

    [PermissionAuthorization("GetPermissionByName")]
    [HttpGet("name")]
    public async Task<IActionResult> Get([FromQuery] GetPermissionByNameQuery request)
    {
        var result = await _mediator.Send(request);
        return StatusCode(result.StatusCode, result);
    }

    [PermissionAuthorization("GetPermissionById")]
    [HttpGet]
    [Route("id")]
    public async Task<IActionResult> Get([FromQuery] GetPermissionByIdQuery request)
    {
        var result = await _mediator.Send(request);
        return StatusCode(result.StatusCode, result);
    }

    [PermissionAuthorization("CreatePermission")]
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreatePermissionCommand request)
    {
        var result = await _mediator.Send(request);
        return StatusCode(result.StatusCode, result);
    }
}