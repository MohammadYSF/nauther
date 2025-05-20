using AutoMapper;
using MediatR;
using Nauther.Framework.Shared.Responses;
using Nauther.Identity.Application.Services.Interfaces;

namespace Nauther.Identity.Application.Features.UserGroup.Commands.CreateUserRole;

public class CreateUserGroupCommandHandler(IMapper mapper, IUserGroupService userGroupService)
    : IRequestHandler<CreateUserGroupCommand, BaseResponse<IList<CreateUserGroupCommandResponse>>>
{
    private readonly IMapper _mapper = mapper;
    private readonly IUserGroupService _userGroupService = userGroupService;

    public async Task<BaseResponse<IList<CreateUserGroupCommandResponse>>> Handle(CreateUserGroupCommand request,
        CancellationToken cancellationToken)
    {
        var userGroups = await _userGroupService.AddUserGroups(request.CreateUserGroupDtos, cancellationToken);
        return new BaseResponse<IList<CreateUserGroupCommandResponse>>
        {
            StatusCode = userGroups.StatusCode,
            Message = userGroups.Message,
            Data = userGroups.Data
        };
    }
}