using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nauther.Framework.RestApi.Attributes;
using Nauther.Identity.Application.Features.Auth.Commands.Register;
using Nauther.Identity.Application.Features.User.Commands.CheckPassword;
using Nauther.Identity.Application.Features.User.Commands.DeleteUser;
using Nauther.Identity.Application.Features.User.Commands.EditUser;
using Nauther.Identity.Application.Features.User.Queries.GetAllUserPermissions;
using Nauther.Identity.Application.Features.User.Queries.GetUserDetail;
using Nauther.Identity.Application.Features.User.Queries.GetUsersList;

namespace Nauther.Identity.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
// [Authorize(AuthenticationSchemes = "Bearer")]

public class UserController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    //[PermissionAuthorization("GetAllUsers")]
    [HttpGet("external")]
    public async Task<IActionResult> Get([FromQuery] GetExternalUsersListQuery request)
    {
        var result = await _mediator.Send(request);
        return StatusCode(result.StatusCode, result);
    }

}