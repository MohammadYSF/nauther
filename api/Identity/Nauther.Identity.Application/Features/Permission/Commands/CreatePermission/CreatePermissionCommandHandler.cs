using AutoMapper;
using MediatR;
using Nauther.Framework.Application.Services.RequestValidatorService;
using Nauther.Framework.Shared.Responses;
using Nauther.Identity.Application.Features.Permission.Commands.EditPermission;
using Nauther.Identity.Application.Services.Interfaces;

namespace Nauther.Identity.Application.Features.Permission.Commands.CreatePermission;

public class CreatePermissionCommandHandler(IPermissionService _permissionService,
    IRequestValidator _requestValidator,
    IMapper _mapper)
    : IRequestHandler<CreatePermissionCommand, BaseResponse<CreatePermissionCommandResponse>>
{
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



public class EditPermissionCommandHandler(IPermissionService permissionService,
    IRequestValidator requestValidator,
    IMapper _mapper)
    : IRequestHandler<EditPermissionCommand, BaseResponse<EditPermissionCommandResponse>>
{
    private readonly IPermissionService _permissionService = permissionService;
    private readonly IRequestValidator _requestValidator = requestValidator;

    public async Task<BaseResponse<EditPermissionCommandResponse>> Handle(EditPermissionCommand request,
        CancellationToken cancellationToken)
    {
        var validationResponse =
            await _requestValidator.ValidateAsync<EditPermissionCommand, EditPermissionCommandValidator>(request);
        if (validationResponse != null)
            return new BaseResponse<EditPermissionCommandResponse>
            {
                StatusCode = validationResponse.StatusCode,
                Message = validationResponse.Message,
                ValidationErrors = validationResponse.ValidationErrors
            };

        var newPermission = await _permissionService.EditPermission(request, cancellationToken);
        return new BaseResponse<EditPermissionCommandResponse>()
        {
            StatusCode = newPermission.StatusCode,
            Message = newPermission.Message,
            Data = newPermission.Data
        };
    }
}
