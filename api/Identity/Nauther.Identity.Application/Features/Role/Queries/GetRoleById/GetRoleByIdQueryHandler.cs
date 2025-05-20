using AutoMapper;
using MediatR;
using Nauther.Framework.Shared.Responses;
using Nauther.Identity.Application.Services.Interfaces;

namespace Nauther.Identity.Application.Features.Role.Queries.GetRoleById;

public class RolesByIdQueryHandler(IRoleService roleService, IMapper mapper) :
    IRequestHandler<GetRoleByIdQuery, BaseResponse<GetRolesQueryResponse>>
{
    private readonly IRoleService _roleService = roleService;
    private readonly IMapper _mapper = mapper;

    public async Task<BaseResponse<GetRolesQueryResponse>> Handle(GetRoleByIdQuery request,
        CancellationToken cancellationToken)
    {
        var role = await _roleService.GetRoleById(request.Id, cancellationToken);
        return new BaseResponse<GetRolesQueryResponse>()
        {
            StatusCode = role.StatusCode,
            Message = role.Message,
            Data = role.Data
        };
    }
}