using MediatR;
using Microsoft.AspNetCore.Mvc;
using Nauther.Identity.Application.Features.Auth.Commands.Register;
using Nauther.Identity.Application.Features.User.Commands.DeleteUser;
using Nauther.Identity.Application.Features.User.Commands.EditUser;
using Nauther.Identity.Application.Features.User.Queries.GetAllUserPermissions;
using Nauther.Identity.Application.Features.User.Queries.GetUserDetail;
using Nauther.Identity.Application.Features.User.Queries.GetUsersList;

namespace Nauther.Identity.Api.Controllers;

[Route("api/[controller]")]
public class AdminController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpPost]
    public async Task<IActionResult> Register([FromBody] Dima_RegisterUserCommand_Dto request)
    {
        var command = new Dima_RegisterUserCommand(request, true);
        var result = await _mediator.Send(command);
        return StatusCode(result.StatusCode, result);
    }
    [HttpPut]
    public async Task<IActionResult> Edit([FromBody] EditUserCommand request)
    {
        var result = await _mediator.Send(request);
        return StatusCode(result.StatusCode, result);
    }
    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody] DeleteUserCommand request)
    {
        var result = await _mediator.Send(request);
        return StatusCode(result.StatusCode, result);
    }
    [HttpGet("{username}/permission")]
    public async Task<IActionResult> GetAllPUserermisions(string username)
    {
        GetAllUserPermissionQuery request = new()
        {
            Username = username
        };
        var result = await _mediator.Send(request);
        return StatusCode(result.StatusCode, result);
    }
    //[PermissionAuthorization("GetUserDetail")]
    [HttpGet("detail")]
    public async Task<IActionResult> Get([FromQuery] GetUserDetailQuery request)
    {
        var result = await _mediator.Send(request);
        return StatusCode(result.StatusCode, result);
    }
    [HttpGet("all")]
    public async Task<IActionResult> Get([FromQuery] GetUsersListQuery request)
    {
        var result = await _mediator.Send(request);
        return StatusCode(result.StatusCode, result);
    }
}