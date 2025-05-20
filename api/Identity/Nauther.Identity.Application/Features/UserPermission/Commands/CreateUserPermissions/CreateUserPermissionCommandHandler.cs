using AutoMapper;
using MediatR;
using Nauther.Framework.Shared.Responses;
using Nauther.Identity.Application.Services.Interfaces;

namespace Nauther.Identity.Application.Features.UserPermission.Commands.CreateUserPermissions;

public class CreateUserPermissionCommandHandler(IMapper mapper, IUserPermissionService userPermissionService)
    : IRequestHandler<CreateUserPermissionCommand,
        BaseResponse<IList<CreateUserPermissionCommandResponse>>>
{
    private readonly IMapper _mapper = mapper;
    private readonly IUserPermissionService _userPermissionService = userPermissionService;

    public async Task<BaseResponse<IList<CreateUserPermissionCommandResponse>>> Handle(CreateUserPermissionCommand request,
        CancellationToken cancellationToken)
    {
        var userPermissions =
            await _userPermissionService.AddUserPermissions(request.CreateUserPermissionDtos, cancellationToken);
        return new BaseResponse<IList<CreateUserPermissionCommandResponse>>()
        {
            StatusCode = userPermissions.StatusCode,
            Message = userPermissions.Message,
            Data = userPermissions.Data
        };
    }
}