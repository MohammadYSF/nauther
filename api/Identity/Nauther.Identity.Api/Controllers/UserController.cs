using MediatR;
using Microsoft.AspNetCore.Mvc;
using Nauther.Framework.RestApi.Attributes;
using Nauther.Identity.Application.Features.Auth.Commands.Register;
using Nauther.Identity.Application.Features.User.Commands.DeleteUser;
using Nauther.Identity.Application.Features.User.Commands.EditUser;
using Nauther.Identity.Application.Features.User.Queries.GetUserDetail;
using Nauther.Identity.Application.Features.User.Queries.GetUsersList;

namespace Nauther.Identity.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] Dima_RegisterUserCommand request)
    {
        var result = await _mediator.Send(request);
        return StatusCode(result.StatusCode, result);
    }
    [HttpPut("edit")]
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
    //[PermissionAuthorization("GetAllUsers")]
    [HttpGet("external")]
    public async Task<IActionResult> Get([FromQuery] GetExternalUsersListQuery request)
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

    //[PermissionAuthorization("GetUserDetail")]
    [HttpGet("detail")]
    public async Task<IActionResult> Get([FromQuery] GetUserDetailQuery request)
    {
        var result = await _mediator.Send(request);
        return StatusCode(result.StatusCode, result);
    }

}