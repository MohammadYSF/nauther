using Nauther.Framework.Application.Common.DTOs;
using Nauther.Framework.Shared.Responses;
using Nauther.Identity.Application.Features.Permission.Commands.CreatePermission;
using Nauther.Identity.Application.Features.Permission.Commands.EditPermission;
using Nauther.Identity.Application.Features.Permission.Queries;

namespace Nauther.Identity.Application.Services.Interfaces;

public interface IPermissionService
{

    Task<BaseResponse<IList<GetPermissionsQueryResponse>?>> GetPermissionsList(PaginationListDto paginationListDto,CancellationToken cancellationToken);
    Task<BaseResponse<GetPermissionsQueryResponse?>> GetPermissionById(Guid id, CancellationToken cancellationToken);
    Task<BaseResponse<IList<GetPermissionsQueryResponse>?>> GetPermissionByName(string name, CancellationToken cancellationToken);
    Task<BaseResponse<CreatePermissionCommandResponse>> AddPermission(CreatePermissionCommand dto,
        CancellationToken cancellationToken);
    Task<BaseResponse<EditPermissionCommandResponse>> EditPermission(EditPermissionCommand dto,
    CancellationToken cancellationToken);
}