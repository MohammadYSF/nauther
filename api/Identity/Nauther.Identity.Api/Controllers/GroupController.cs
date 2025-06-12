using MediatR;
using Microsoft.AspNetCore.Mvc;
using Nauther.Framework.RestApi.Attributes;
using Nauther.Identity.Application.Features.Group.Commands.CreateGroup;
using Nauther.Identity.Application.Features.Group.Queries.GetGroupsList;
using Nauther.Identity.Application.Features.Group.Queries.GetRoleById;
using Nauther.Identity.Application.Features.Group.Queries.GetRoleByName;

namespace Nauther.Identity.Api.Controllers;

[Route("api/[controller]")]
[ApiController]

public class GroupController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;
    
    [PermissionAuthorization("GetAllGroups")]
    [HttpGet("all")]
    public async Task<IActionResult> Get([FromQuery] GetGroupsListQuery request)
    {
        var result = await _mediator.Send(request);
        return StatusCode(result.StatusCode, result);
    }

    [PermissionAuthorization("GetGroupByName")]
    [HttpGet("name")]
    public async Task<IActionResult> Get([FromQuery] GetGroupByNameQuery request)
    {
        var result = await _mediator.Send(request);
        return StatusCode(result.StatusCode, result);
    }

    [PermissionAuthorization("GetGroupById")]
    [HttpGet]
    [Route("id")]
    public async Task<IActionResult> Get([FromQuery] GetGroupByIdQuery request)
    {
        var result = await _mediator.Send(request);
        return StatusCode(result.StatusCode, result);
    }
    
    [PermissionAuthorization("CreateGroup")]
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateGroupCommand request)
    {
        var result = await _mediator.Send(request);
        return StatusCode(result.StatusCode, result);
    }
}