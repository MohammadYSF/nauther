using AutoMapper;
using MediatR;
using Nauther.Framework.Shared.Responses;
using Nauther.Identity.Application.Services.Interfaces;

namespace Nauther.Identity.Application.Features.Role.Queries.GetRolesList;

public class GetRolesListQueryHandler(IRoleService roleService,IMapper mapper) :
    IRequestHandler<GetRolesListQuery, BaseResponse<IList<GetRolesQueryResponse>>>
{
    private readonly IRoleService _roleService = roleService;
    private readonly IMapper _mapper = mapper;

    public async Task<BaseResponse<IList<GetRolesQueryResponse>>> Handle(GetRolesListQuery request,
        CancellationToken cancellationToken)
    {
        var roles = await _roleService.GetRolesList(request,cancellationToken);
        return new BaseResponse<IList<GetRolesQueryResponse>>()
        {
            StatusCode = roles.StatusCode,
            Message = roles.Message,
            Data = roles.Data
        };
    }
}