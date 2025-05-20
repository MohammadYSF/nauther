using AutoMapper;
using MediatR;
using Nauther.Framework.Shared.Responses;
using Nauther.Identity.Application.Services.Interfaces;

namespace Nauther.Identity.Application.Features.Group.Queries.GetRoleById;

public class GetGroupByIdQueryHandler(IGroupService groupService, IMapper mapper) :
    IRequestHandler<GetGroupByIdQuery, BaseResponse<GetGroupsQueryResponse>>
{
    private readonly IGroupService _groupService = groupService;
    private readonly IMapper _mapper = mapper;

    public async Task<BaseResponse<GetGroupsQueryResponse>> Handle(GetGroupByIdQuery request,
        CancellationToken cancellationToken)
    {
        var group = await _groupService.GetGroupById(request.Id, cancellationToken);
        return new BaseResponse<GetGroupsQueryResponse>()
        {
            StatusCode = group.StatusCode,
            Message = group.Message,
            Data = group.Data
        };
    }
}