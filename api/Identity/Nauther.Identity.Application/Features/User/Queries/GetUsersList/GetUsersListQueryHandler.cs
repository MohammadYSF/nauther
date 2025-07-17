using auther.Identity.Application.Services.Interfaces;
using AutoMapper;
using MediatR;
using Nauther.Framework.Shared.Responses;
using Nauther.Identity.Application.Services.Interfaces;
using Newtonsoft.Json.Linq;

namespace Nauther.Identity.Application.Features.User.Queries.GetUsersList;

public class GetUsersListQueryHandler(IUserService userService, IMapper mapper) :
    IRequestHandler<GetUsersListQuery, BaseResponse>,
    IRequestHandler<GetExternalUsersListQuery, BaseResponse>
{
    private readonly IUserService _userService = userService;
    private readonly IMapper _mapper = mapper;

    public async Task<BaseResponse> Handle(GetUsersListQuery request,
        CancellationToken cancellationToken)
    {
        var users = await _userService.GetUsersList(request, cancellationToken);
        return users;
    }
    public async Task<BaseResponse> Handle(GetExternalUsersListQuery request,
        CancellationToken cancellationToken)
    {
        var users = await _userService.GetExternalUsersList(request, cancellationToken);
        return users;
    }
}