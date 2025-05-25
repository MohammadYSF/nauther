using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Nauther.Framework.Shared.Responses;

namespace Nauther.Identity.Application.Features.User.Commands.EditUser
{
    public class EditUserCommand : IRequest<BaseResponse>
    {
        public string Id { get; set; }
        public string? Password { get; set; }
        public string? ConfirmPassword { get; set; }
        public List<Guid> Roles { get; set; } = [];
        public List<Guid> Permissions { get; set; } = [];
    }
}
