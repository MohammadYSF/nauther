using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Nauther.Framework.Shared.Responses;

namespace Nauther.Identity.Application.Features.User.Commands.DeleteUser
{
    public class DeleteUserCommand : IRequest<BaseResponse>
    {
        public List<string> Ids { get; set; } = [];
    }
}
