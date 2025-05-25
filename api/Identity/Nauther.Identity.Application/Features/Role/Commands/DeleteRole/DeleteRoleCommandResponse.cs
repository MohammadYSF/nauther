using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nauther.Identity.Application.Features.Role.Commands.DeleteRole
{
    public class DeleteRoleCommandResponse
    {
        public List<Guid> Ids { get; set; } = [];
    }
}
