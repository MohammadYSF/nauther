
using AutoMapper;
using MediatR;
using Nauther.Framework.Shared.Responses;
using Nauther.Identity.Application.Services.Interfaces;

namespace Nauther.Identity.Application.Features.Role.Queries.GetRoleByName;

public class GetRoleByNameQueryHandler(IRoleService roleService, IMapper mapper) :
    IRequestHandler<GetRoleByNameQuery, BaseResponse<IList<GetRolesQueryResponse>>>
{
    private readonly IRoleService _roleService = roleService;
    private readonly IMapper _mapper = mapper;

    public async Task<BaseResponse<IList<GetRolesQueryResponse>>> Handle(GetRoleByNameQuery request,
        CancellationToken cancellationToken)
    {
        var role = await _roleService.GetRoleByName(request.Name, cancellationToken);
        return new BaseResponse<IList<GetRolesQueryResponse>>()
        {
            StatusCode = role.StatusCode,
            Message = role.Message,
            Data = role.Data
        };
    }
}