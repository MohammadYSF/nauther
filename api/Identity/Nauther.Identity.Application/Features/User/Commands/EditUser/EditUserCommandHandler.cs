using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Nauther.Framework.Application.Services.RequestValidatorService;
using Nauther.Framework.Shared.Responses;
using Nauther.Identity.Application.Features.Auth.Commands.Register;
using Nauther.Identity.Application.Services.Implementations;
using Nauther.Identity.Application.Services.Interfaces;

namespace Nauther.Identity.Application.Features.User.Commands.EditUser
{
    public class EditUserCommandHandler(IUserService userService, IRequestValidator requestValidator) : IRequestHandler<EditUserCommand, BaseResponse>
    {
        private readonly IUserService _userService = userService;
        private readonly IRequestValidator _requestValidator = requestValidator;
        public async Task<BaseResponse> Handle(EditUserCommand request, CancellationToken cancellationToken)
        {
            var validationResponse =
         await _requestValidator.ValidateAsync<EditUserCommand, EditUserCVommandValidator>(request);
            if (validationResponse != null)
                return new BaseResponse
                {
                    StatusCode = validationResponse.StatusCode,
                    Message = validationResponse.Message,
                    ValidationErrors = validationResponse.ValidationErrors
                };

            var newUser = await _userService.Edit(request, cancellationToken);
            return newUser;
        }
    }
}
