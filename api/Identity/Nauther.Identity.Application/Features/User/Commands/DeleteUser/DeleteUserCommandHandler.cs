using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using auther.Identity.Application.Services.Interfaces;
using MediatR;
using Nauther.Framework.Shared.Responses;
using Nauther.Identity.Application.Services.Interfaces;

namespace Nauther.Identity.Application.Features.User.Commands.DeleteUser
{
    public class DeleteUserCommandHandler(IUserService userService) : IRequestHandler<DeleteUserCommand, BaseResponse>
    {
        private readonly IUserService _userService = userService;
        public async Task<BaseResponse> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            return await _userService.Delete(request, cancellationToken);
        }
    }
}
