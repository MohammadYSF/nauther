using MediatR;
using Nauther.Framework.Shared.Responses;

namespace Nauther.Identity.Application.Features.Group.Commands.CreateGroup;

public class CreateGroupCommand : IRequest<BaseResponse<CreateGroupCommandResponse>>
{
    public string Name { get; set; }
}