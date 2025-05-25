using Nauther.Framework.Application.Common.DTOs;
using Nauther.Framework.Shared.Responses;
using Nauther.Identity.Application.Features.Role.Commands.CreateRole;
using Nauther.Identity.Application.Features.Role.Commands.DeleteRole;
using Nauther.Identity.Application.Features.Role.Queries;

namespace Nauther.Identity.Application.Services.Interfaces;

public interface IRoleService
{
    Task<BaseResponse> DeleteRoles(DeleteRoleCommand dto, CancellationToken cancellationToken);
    Task<BaseResponse<IList<GetRolesQueryResponse>?>> GetRolesList(PaginationListDto paginationListDto, CancellationToken cancellationToken);
    Task<BaseResponse<GetRolesQueryResponse?>> GetRoleById(Guid id, CancellationToken cancellationToken);
    Task<BaseResponse<IList<GetRolesQueryResponse>?>> GetRoleByName(string name, CancellationToken cancellationToken);
    Task<BaseResponse<CreateRoleCommandResponse>> AddRole(CreateRoleCommand dto, CancellationToken cancellationToken);
}