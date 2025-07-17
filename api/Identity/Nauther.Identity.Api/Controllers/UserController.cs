using MediatR;
using Microsoft.AspNetCore.Mvc;
using Nauther.Identity.Application.Features.Auth.Commands.Register;
using Nauther.Identity.Application.Features.User.Queries.GetUsersList;
using Nauther.Identity.Infrastructure.Attributes;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

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
        var json = JsonConvert.SerializeObject(result, new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            Formatting = Formatting.None
        });
        return new ContentResult
        {
            Content = json,
            ContentType = "application/json",
            StatusCode = result.StatusCode // or 400, 403, 418 (because you're a teapot), etc.
        };
        return StatusCode(result.StatusCode, result);
    }
    //usage : X-API-KEY: SuperSecretKey123
    [HttpPost("register")]
    [ApiKey]
    public async Task<IActionResult> Register([FromHeader(Name = "X-API-KEY")] string apiKey,[FromBody] Dima_RegisterUserCommand_Dto request)
    {
        var command = new Dima_RegisterUserCommand(request, false);

        var result = await _mediator.Send(command);
        return StatusCode(result.StatusCode, result);
    }
}