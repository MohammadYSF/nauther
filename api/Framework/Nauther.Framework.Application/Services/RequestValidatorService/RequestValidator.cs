using FluentValidation;
using Microsoft.AspNetCore.Http;
using Nauther.Framework.Shared.Constants;
using Nauther.Framework.Shared.Responses;

namespace Nauther.Framework.Application.Services.RequestValidatorService;

public class RequestValidator : IRequestValidator
{
    public async Task<BaseResponse?> ValidateAsync<TRequest, TValidator>(TRequest request)
        where TValidator : AbstractValidator<TRequest>, new()
    {
        var validator = new TValidator();
        var validationResult = await validator.ValidateAsync(request);

        var errors = validationResult.Errors
            .Select(x => new ValidationError
            {
                Key = x.PropertyName,
                Value = x.ErrorMessage
            })
            .ToList();
        if (errors.Count != 0)
            return new BaseResponse()
            {
                StatusCode = StatusCodes.Status400BadRequest,
                Message = GeneralMessages.BadRequest,
                ValidationErrors = errors,
                Data = null
            };
        return null;
    }
}