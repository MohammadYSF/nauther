using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Nauther.Framework.Shared.Responses;

namespace Nauther.Identity.Application.Features.User.Commands.CheckPassword
{
    public class CheckPasswordCommand:IRequest<BaseResponse<CheckPasswordResponse>>
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
