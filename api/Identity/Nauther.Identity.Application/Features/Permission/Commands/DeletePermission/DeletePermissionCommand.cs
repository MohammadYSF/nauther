using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Nauther.Framework.Shared.Responses;

namespace Nauther.Identity.Application.Features.Permission.Commands.De_etePermission
{
    public class DeletePermissionCommand : IRequest<BaseResponse>
    {
        public List<Guid> Ids { get; set; }
    }
}
