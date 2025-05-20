using Nauther.Framework.Application.Common.DTOs;
using Nauther.Framework.Shared.Responses;
using Nauther.Identity.Application.Features.Group.Commands.CreateGroup;
using Nauther.Identity.Application.Features.Group.Queries;

namespace Nauther.Identity.Application.Services.Interfaces;

public interface IGroupService
{
    Task<BaseResponse<IList<GetGroupsQueryResponse>?>> GetGroupsList(PaginationListDto paginationListDto,CancellationToken cancellationToken);
    Task<BaseResponse<GetGroupsQueryResponse?>> GetGroupById(Guid id, CancellationToken cancellationToken);
    Task<BaseResponse<IList<GetGroupsQueryResponse>?>> GetGroupByName(string name, CancellationToken cancellationToken);
    Task<BaseResponse<CreateGroupCommandResponse>> AddGroup(CreateGroupCommand dto, CancellationToken cancellationToken);
}