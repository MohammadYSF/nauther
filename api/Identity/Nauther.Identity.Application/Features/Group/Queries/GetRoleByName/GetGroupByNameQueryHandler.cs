using AutoMapper;
using MediatR;
using Nauther.Framework.Shared.Responses;
using Nauther.Identity.Application.Services.Interfaces;

namespace Nauther.Identity.Application.Features.Group.Queries.GetRoleByName;

public class GetGroupByNameQueryHandler(IGroupService groupService, IMapper mapper) :
    IRequestHandler<GetGroupByNameQuery, BaseResponse<IList<GetGroupsQueryResponse>>>
{
    private readonly IGroupService _groupService = groupService;
    private readonly IMapper _mapper = mapper;

    public async Task<BaseResponse<IList<GetGroupsQueryResponse>>> Handle(GetGroupByNameQuery request,
        CancellationToken cancellationToken)
    {
        var group = await _groupService.GetGroupByName(request.Name, cancellationToken);
        return new BaseResponse<IList<GetGroupsQueryResponse>>()
        {
            StatusCode = group.StatusCode,
            Message = group.Message,
            Data = group.Data
        };
    }
}