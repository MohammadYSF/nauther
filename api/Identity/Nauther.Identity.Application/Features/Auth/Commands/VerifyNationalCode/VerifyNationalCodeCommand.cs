using MediatR;
using Nauther.Framework.Shared.Responses;

namespace Nauther.Identity.Application.Features.Auth.Commands.VerifyNationalCode;

public class VerifyNationalCodeCommand : IRequest<BaseResponse<VerifyNationalCodeCommandResponse>>
{
    public string NationalCode { get; set; }
}