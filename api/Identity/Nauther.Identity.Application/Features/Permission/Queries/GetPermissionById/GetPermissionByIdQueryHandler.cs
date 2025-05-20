using AutoMapper;
using MediatR;
using Nauther.Framework.Shared.Responses;
using Nauther.Identity.Application.Services.Interfaces;

namespace Nauther.Identity.Application.Features.Permission.Queries.GetPermissionById;

public class PermissionsVmListQueryHandler(IPermissionService permissionService, IMapper mapper) :
    IRequestHandler<GetPermissionByIdQuery, BaseResponse<GetPermissionsQueryResponse>>
{
    private readonly IPermissionService _permissionService= permissionService;
    private readonly IMapper _mapper = mapper;

    public async Task<BaseResponse<GetPermissionsQueryResponse>> Handle(GetPermissionByIdQuery request,
        CancellationToken cancellationToken)
    {
        var permission = await _permissionService.GetPermissionById(request.Id, cancellationToken);
        return new BaseResponse<GetPermissionsQueryResponse>()
        {
            StatusCode = permission.StatusCode,
            Message = permission.Message,
            Data = permission.Data
        };
    }
}