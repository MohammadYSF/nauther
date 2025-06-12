using auther.Identity.Application.Services.Interfaces;
using AutoMapper;
using MediatR;
using Nauther.Framework.Application.Services.RequestValidatorService;
using Nauther.Framework.Shared.Responses;
using Nauther.Identity.Application.Features.User.Queries.GetUserDetail;

namespace Nauther.Identity.Application.Features.User.Queries.GetAllUserPermissions;

public class GetAllUserPermissionQueryHandler(IUserService userService, IMapper mapper, IRequestValidator requestValidator) :
    IRequestHandler<GetAllUserPermissionQuery, BaseResponse<List<string>>>
{
    private readonly IUserService _userService = userService;
    private readonly IMapper _mapper = mapper;
    private readonly IRequestValidator _requestValidator = requestValidator;

    public async Task<BaseResponse<List<string>>> Handle(GetAllUserPermissionQuery request, CancellationToken cancellationToken)
    {
        var res = await _userService.GetAllPermissionsByUsername(request.Id,
            cancellationToken);
        return res;
    }
}