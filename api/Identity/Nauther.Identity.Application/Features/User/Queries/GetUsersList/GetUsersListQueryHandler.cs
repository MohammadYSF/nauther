using AutoMapper;
using MediatR;
using Nauther.Framework.Shared.Responses;
using Nauther.Identity.Application.Services.Interfaces;

namespace Nauther.Identity.Application.Features.User.Queries.GetUsersList;

public class GetUsersListQueryHandler(IUserService userService,IMapper mapper) :
    IRequestHandler<GetUsersListQuery, BaseResponse>
{
    private readonly IUserService _userService = userService;
    private readonly IMapper _mapper = mapper;

    public async Task<BaseResponse> Handle(GetUsersListQuery request,
        CancellationToken cancellationToken)
    {
        var users = await _userService.GetUsersList(request,cancellationToken);
        return users;
    }
}