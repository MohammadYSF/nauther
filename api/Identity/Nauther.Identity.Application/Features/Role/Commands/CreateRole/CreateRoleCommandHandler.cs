using AutoMapper;
using MediatR;
using Nauther.Framework.Application.Interfaces.IRepositories;
using Nauther.Framework.Application.Services.RequestValidatorService;
using Nauther.Framework.Shared.Responses;
using Nauther.Identity.Application.Services.Interfaces;

namespace Nauther.Identity.Application.Features.Role.Commands.CreateRole;

public class CreateRoleCommandHandler(IRoleService roleService,
    IRequestValidator requestValidator,
    IMapper mapper)
    : IRequestHandler<CreateRoleCommand, BaseResponse<CreateRoleCommandResponse>>
{
    private readonly IRoleService _roleService = roleService;
    private readonly IRequestValidator _requestValidator = requestValidator;
    private readonly IMapper _mapper = mapper;
    
    public async Task<BaseResponse<CreateRoleCommandResponse>> Handle(CreateRoleCommand request,
        CancellationToken cancellationToken)
    {
        var validationResponse =
            await _requestValidator.ValidateAsync<CreateRoleCommand, CreateRoleCommandValidator>(request);
        if (validationResponse != null)
            return new BaseResponse<CreateRoleCommandResponse>
            {
                StatusCode = validationResponse.StatusCode,
                Message = validationResponse.Message,
                ValidationErrors = validationResponse.ValidationErrors
            };
        
        var newRole = await _roleService.AddRole(request, cancellationToken);
        return new BaseResponse<CreateRoleCommandResponse>()
        {
            StatusCode = newRole.StatusCode,
            Message = newRole.Message,
            Data = newRole.Data
        };
    }
}