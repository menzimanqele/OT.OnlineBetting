using FluentValidation;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace OT.OnlineBetting.Api.Filters;

public sealed class RequestValidation : IActionFilter
{
    private readonly IServiceProvider _serviceProvider;

    public RequestValidation(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
    
    public void OnActionExecuting(ActionExecutingContext context)
    {
        foreach (var argument in context.ActionArguments.Values)
        {
            if(argument is null) continue;
            var validatorType = typeof(IValidator<>).MakeGenericType(argument.GetType());

            var validator = _serviceProvider.GetService(validatorType);  //(IValidator)Activator.CreateInstance(validatorType);

            if (validator is not IValidator validatorInstance) continue;
            var result = validatorInstance.Validate(new ValidationContext<object>(argument));

            if (result.IsValid) continue;
            var errors = JsonConvert.SerializeObject(result.Errors.Select(x => x.ErrorMessage));
            throw new BadHttpRequestException(errors);
        }
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
       
    }
}