using AutoMapper;
using MediatR;
using Nauther.Framework.Shared.Responses;
using Nauther.Identity.Application.Services.Interfaces;

namespace Nauther.Identity.Application.Features.Group.Queries.GetGroupsList;

public class GetGroupsListQueryHandler(IGroupService groupService,IMapper mapper) :
    IRequestHandler<GetGroupsListQuery, BaseResponse<IList<GetGroupsQueryResponse>>>
{
    private readonly IGroupService _groupService = groupService;
    private readonly IMapper _mapper = mapper;

    public async Task<BaseResponse<IList<GetGroupsQueryResponse>>> Handle(GetGroupsListQuery request,
        CancellationToken cancellationToken)
    {
        var groups = await _groupService.GetGroupsList(request,cancellationToken);
        return new BaseResponse<IList<GetGroupsQueryResponse>>()
        {
            StatusCode = groups.StatusCode,
            Message = groups.Message,
            Data = groups.Data
        };
    }
}