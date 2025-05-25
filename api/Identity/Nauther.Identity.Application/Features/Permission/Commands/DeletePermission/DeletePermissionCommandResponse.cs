using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Nauther.Framework.Shared.Responses;

namespace Nauther.Identity.Application.Features.Permission.Commands.DeletePermission
{
    public class DeletePermissionCommandResponse
    {
        public List<Guid> Ids { get; set; } = [];
    }
}
