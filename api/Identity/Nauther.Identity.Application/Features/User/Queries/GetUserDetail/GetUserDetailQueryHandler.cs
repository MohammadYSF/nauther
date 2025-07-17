using auther.Identity.Application.Services.Interfaces;
using AutoMapper;
using MediatR;
using Nauther.Framework.Application.Services.RequestValidatorService;
using Nauther.Framework.Shared.Responses;

namespace Nauther.Identity.Application.Features.User.Queries.GetUserDetail;

public class GetUserDetailQueryHandler(IUserService userService, IMapper mapper, IRequestValidator requestValidator) :
    IRequestHandler<GetUserDetailQuery, BaseResponse>
{
    private readonly IUserService _userService = userService;
    private readonly IMapper _mapper = mapper;
    private readonly IRequestValidator _requestValidator = requestValidator;

    public async Task<BaseResponse> Handle(GetUserDetailQuery request, CancellationToken cancellationToken)
    {
        var user = await _userService.GetUserDetails(request.Id, request.Username, request.PhoneNumber,
            cancellationToken);
        return user;
    }
}