using MediatR;
using Nauther.Framework.Shared.Responses;

namespace Nauther.Identity.Application.Features.User.Queries.GetUserDetail;

public class GetUserDetailQuery : IRequest<BaseResponse<GetUserDetailQueryResponse>>
{
    public string Id { get; set; }
    public string? Username { get; set; }
    public string? PhoneNumber { get; set; }
}