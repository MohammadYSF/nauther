using AutoMapper;
using MediatR;
using Nauther.Framework.Application.Interfaces.IRepositories;
using Nauther.Framework.Application.Services.RequestValidatorService;
using Nauther.Framework.Shared.Responses;
using Nauther.Identity.Application.Services.Interfaces;

namespace Nauther.Identity.Application.Features.Group.Commands.CreateGroup;

public class CreateGroupCommandHandler(IGroupService groupService, IRequestValidator requestValidator,
    IMapper mapper)
    : IRequestHandler<CreateGroupCommand, BaseResponse<CreateGroupCommandResponse>>
{
    private readonly IGroupService _groupService = groupService;
    private readonly IRequestValidator _requestValidator = requestValidator;
    private readonly IMapper _mapper = mapper;
    
    public async Task<BaseResponse<CreateGroupCommandResponse>> Handle(CreateGroupCommand request, CancellationToken cancellationToken)
    {
        var validationResponse = 
            await _requestValidator.ValidateAsync<CreateGroupCommand, CreateGroupCommandValidator>(request);
        if (validationResponse != null)
            return new BaseResponse<CreateGroupCommandResponse>()
            {
                StatusCode = validationResponse.StatusCode,
                Message = validationResponse.Message,
                ValidationErrors = validationResponse.ValidationErrors
            };
        
        var newGroup = await _groupService.AddGroup(request, cancellationToken);
        return new BaseResponse<CreateGroupCommandResponse>()
        {
            StatusCode = newGroup.StatusCode,
            Message = newGroup.Message,
            Data = newGroup.Data
        };
    }
}