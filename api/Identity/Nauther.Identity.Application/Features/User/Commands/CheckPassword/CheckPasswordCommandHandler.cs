using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using auther.Identity.Application.Services.Interfaces;
using MediatR;
using Nauther.Framework.Shared.Responses;

namespace Nauther.Identity.Application.Features.User.Commands.CheckPassword
{
    public class CheckPasswordCommandHandler(IUserService userService) : IRequestHandler<CheckPasswordCommand, BaseResponse<CheckPasswordResponse>>
    {
        private readonly IUserService _userService = userService;
        public async Task<BaseResponse<CheckPasswordResponse>> Handle(CheckPasswordCommand request, CancellationToken cancellationToken)
        {
            return await _userService.CheckPassword(request, cancellationToken);
        }
    }
}
