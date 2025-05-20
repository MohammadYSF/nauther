using FluentValidation;
using Nauther.Framework.Shared.Responses;

namespace Nauther.Framework.Application.Services.RequestValidatorService;

public interface IRequestValidator
{
    Task<BaseResponse?> ValidateAsync<TRequest, TValidator>(TRequest request)
        where TValidator : AbstractValidator<TRequest>, new();
}