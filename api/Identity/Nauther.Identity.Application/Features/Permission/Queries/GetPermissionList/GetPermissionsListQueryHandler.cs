using AutoMapper;
using MediatR;
using Nauther.Framework.Shared.Responses;
using Nauther.Identity.Application.Services.Interfaces;

namespace Nauther.Identity.Application.Features.Permission.Queries.GetPermissionList;

public class GetPermissionsListQueryHandler(IPermissionService permissionService,IMapper mapper) :
    IRequestHandler<GetPermissionsListQuery, BaseResponse<IList<GetPermissionsQueryResponse>>>
{
    private readonly IPermissionService _permissionService = permissionService;
    private readonly IMapper _mapper = mapper;

    public async Task<BaseResponse<IList<GetPermissionsQueryResponse>>> Handle(GetPermissionsListQuery request,
        CancellationToken cancellationToken)
    {
        var permissions = await _permissionService.GetPermissionsList(request,cancellationToken);
        return permissions;
    }
}