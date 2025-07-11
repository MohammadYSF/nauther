using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Nauther.Framework.RestApi.Attributes;
using Nauther.Identity.Api.Attributes;
using Nauther.Identity.Application.Features.Auth.Commands.LoginWithPassword;
using Nauther.Identity.Application.Features.Auth.Commands.Register;
using Nauther.Identity.Application.Features.Auth.Commands.SendOtp;
using Nauther.Identity.Application.Features.Auth.Commands.VerifyNationalCode;
using Nauther.Identity.Application.Features.Auth.Commands.VerifyOtp;
using Nauther.Identity.Application.Features.User.Commands.CheckPassword;

namespace Nauther.Identity.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;
    
    [HttpPost("send-otp")]
    public async Task<IActionResult> VerifyOtp([FromBody] SendOtpCommand request)
    {
        var result = await _mediator.Send(request);
        return StatusCode(result.StatusCode,result);
    }
    
    [HttpPost("verify-otp")]
    public async Task<IActionResult> VerifyOtp([FromBody] VerifyOtpCommand request)
    {
        var result = await _mediator.Send(request);
        return StatusCode(result.StatusCode,result);
    }
    
    [HttpPost("verify-nationalCode")]
    public async Task<IActionResult> VerifyNationalCode([FromBody] VerifyNationalCodeCommand request)
    {
        var result = await _mediator.Send(request);
        return StatusCode(result.StatusCode,result);
    }
    
    [HttpPost("login/password")]
    public async Task<IActionResult> LoginWithPassword([FromBody] LoginWithPasswordCommand request)
    {
        var result = await _mediator.Send(request);
        return StatusCode(result.StatusCode,result);
    }
    
    [HttpPost("checkPassword")]
    public async Task<IActionResult> CheckPassword([FromBody] CheckPasswordCommand request)
    {
        var result = await _mediator.Send(request);
        return StatusCode(result.StatusCode, result);
    }
    /*[HttpPost("login/otp")]
    public async Task<IActionResult> SendOtp()
    {
        throw new NotImplementedException();
    }*/
}