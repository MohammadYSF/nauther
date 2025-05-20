using AutoMapper;
using MediatR;
using Nauther.Framework.Shared.Responses;
using Nauther.Identity.Application.Services.Interfaces;

namespace Nauther.Identity.Application.Features.UserRole.Commands.CreateUserRole;

public class CreateUserRoleCommandHandler(IMapper mapper, IUserRoleService userRoleService)
    : IRequestHandler<CreateUserRoleCommand, BaseResponse<IList<CreateUserRoleCommandResponse>>>
{
    private readonly IMapper _mapper = mapper;
    private readonly IUserRoleService _userRoleService = userRoleService;

    public async Task<BaseResponse<IList<CreateUserRoleCommandResponse>>> Handle(CreateUserRoleCommand request,
        CancellationToken cancellationToken)
    {
        var userRoles = await _userRoleService.AddUserRoles(request.CreateUserRoleDtos, cancellationToken);
        return new BaseResponse<IList<CreateUserRoleCommandResponse>>
        {
            StatusCode = userRoles.StatusCode,
            Message = userRoles.Message,
            Data = userRoles.Data
        };
    }
}