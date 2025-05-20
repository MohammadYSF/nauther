using AutoMapper;
using MediatR;
using Nauther.Framework.Shared.Responses;
using Nauther.Identity.Application.Services.Interfaces;

namespace Nauther.Identity.Application.Features.GroupPermission.Commands.CreateGroupPermissions;

public class CreateGroupPermissionCommandHandler(IMapper mapper, IGroupPermissionService groupPermissionService)
    : IRequestHandler<CreateGroupPermissionCommand,
        BaseResponse<IList<CreateGroupPermissionCommandResponse>>>
{
    private readonly IMapper _mapper = mapper;
    private readonly IGroupPermissionService _groupPermissionService = groupPermissionService;

    public async Task<BaseResponse<IList<CreateGroupPermissionCommandResponse>>> Handle(CreateGroupPermissionCommand request,
        CancellationToken cancellationToken)
    {
        var groupPermissions =
            await _groupPermissionService.AddGroupPermissions(request.CreateGroupPermissionDtos, cancellationToken);
        return new BaseResponse<IList<CreateGroupPermissionCommandResponse>>()
        {
            StatusCode = groupPermissions.StatusCode,
            Message = groupPermissions.Message,
            Data = groupPermissions.Data
        };
    }
}