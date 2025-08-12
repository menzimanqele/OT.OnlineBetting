using FluentValidation;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace OT.OnlineBetting.Application.Utilities;

public sealed class ValidateRequestModel<TRequest, TRequestValidator>  where TRequestValidator : AbstractValidator<TRequest>
{
    private static readonly IValidator<TRequest> Validator = Activator.CreateInstance<TRequestValidator>();

    public static void Validate(TRequest request)
    {
       var result = Validator.Validate(request);
       if (result.IsValid) return;
       var errors = JsonConvert.SerializeObject(result.Errors.Select(x => x.ErrorMessage));
       throw new BadHttpRequestException(errors);
    }
}