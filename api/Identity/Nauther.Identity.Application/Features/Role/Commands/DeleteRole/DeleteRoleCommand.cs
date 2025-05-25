using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Nauther.Framework.Shared.Responses;

namespace Nauther.Identity.Application.Features.Role.Commands.DeleteRole
{
    public class DeleteRoleCommand : IRequest<BaseResponse>
    {
        public List<Guid> Ids { get; set; } = [];
    }
}
