using AutoMapper;
using MediatR;
using Nauther.Framework.Application.Interfaces.IRepositories;
using Nauther.Framework.Application.Services.RequestValidatorService;
using Nauther.Framework.Shared.Responses;
using Nauther.Identity.Application.Services.Interfaces;

namespace Nauther.Identity.Application.Features.User.Queries.GetUserDetail;

public class GetUserDetailQueryHandler(IUserService userService, IMapper mapper, IRequestValidator requestValidator) :
    IRequestHandler<GetUserDetailQuery, BaseResponse<GetUserDetailQueryResponse>>
{
    private readonly IUserService _userService = userService;
    private readonly IMapper _mapper = mapper;
    private readonly IRequestValidator _requestValidator = requestValidator;

    public async Task<BaseResponse<GetUserDetailQueryResponse>> Handle(GetUserDetailQuery request, CancellationToken cancellationToken)
    {
        var user = await _userService.GetUserDetails(request.Id, request.Username, request.PhoneNumber,
            cancellationToken);
        return new BaseResponse<GetUserDetailQueryResponse>()
        {
            StatusCode = user.StatusCode,
            Message = user.Message,
            Data = user.Data
        };
    }
}