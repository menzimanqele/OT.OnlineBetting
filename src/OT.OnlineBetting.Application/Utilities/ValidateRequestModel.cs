using FluentValidation;

namespace OT.OnlineBetting.Application.Utilities;

public sealed class ValidateRequestModel<TRequest, TRequestValidator>  where TRequestValidator : AbstractValidator<TRequest>
{
    private static readonly IValidator<TRequest> Validator = Activator.CreateInstance<TRequestValidator>();

    public static void Validate(TRequest request)
    {
       var result = Validator.Validate(request);
       if(!result.IsValid) throw new ValidationException(result.Errors);
    }
}