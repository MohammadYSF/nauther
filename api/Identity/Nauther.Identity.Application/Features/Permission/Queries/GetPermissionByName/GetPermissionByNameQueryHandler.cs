using AutoMapper;
using MediatR;
using Nauther.Framework.Application.Interfaces.IRepositories;
using Nauther.Framework.Application.Services.RequestValidatorService;
using Nauther.Framework.Shared.Responses;
using Nauther.Identity.Application.Services.Interfaces;

namespace Nauther.Identity.Application.Features.Permission.Queries.GetPermissionByName;

public class GetPermissionByNameQueryHandler(IPermissionService permissionService, IMapper mapper,
    IRequestValidator requestValidator) :
    IRequestHandler<GetPermissionByNameQuery, BaseResponse<IList<GetPermissionsQueryResponse>>>
{
    private readonly IPermissionService _permissionService = permissionService;
    private readonly IMapper _mapper = mapper;
    private readonly IRequestValidator _requestValidator = requestValidator;

    public async Task<BaseResponse<IList<GetPermissionsQueryResponse>>> Handle(GetPermissionByNameQuery request,
        CancellationToken cancellationToken)
    {
        var permission = await _permissionService.GetPermissionByName(request.Name, cancellationToken);
        return new BaseResponse<IList<GetPermissionsQueryResponse>>()
        {
            StatusCode = permission.StatusCode,
            Message = permission.Message,
            Data = permission.Data
        };
    }
}