using AutoMapper;
using MediatR;
using Nauther.Framework.Application.Interfaces.IRepositories;
using Nauther.Framework.Application.Services.RequestValidatorService;
using Nauther.Framework.Shared.Responses;
using Nauther.Identity.Application.Services.Interfaces;

namespace Nauther.Identity.Application.Features.Permission.Commands.CreatePermission;

public class CreatePermissionCommandHandler(IPermissionService permissionService,
    IRequestValidator requestValidator,
    IMapper mapper)
    : IRequestHandler<CreatePermissionCommand, BaseResponse<CreatePermissionCommandResponse>>
{
    private readonly IPermissionService _permissionService = permissionService;
    private readonly IRequestValidator _requestValidator = requestValidator;
    private readonly IMapper _mapper = mapper;
    
    public async Task<BaseResponse<CreatePermissionCommandResponse>> Handle(CreatePermissionCommand request,
        CancellationToken cancellationToken)
    {
        var validationResponse =
            await _requestValidator.ValidateAsync<CreatePermissionCommand, CreatePermissionCommandValidator>(request);
        if (validationResponse != null)
            return new BaseResponse<CreatePermissionCommandResponse>
            {
                StatusCode = validationResponse.StatusCode,
                Message = validationResponse.Message,
                ValidationErrors = validationResponse.ValidationErrors
            };
        
        var newPermission = await _permissionService.AddPermission(request, cancellationToken);
        return new BaseResponse<CreatePermissionCommandResponse>()
        {
            StatusCode = newPermission.StatusCode,
            Message = newPermission.Message,
            Data = newPermission.Data
        };
    }
}