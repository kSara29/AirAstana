using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Api.Filters;

public class FluentValidationFilter : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext ctx, ActionExecutionDelegate next)
    {
        foreach (var arg in ctx.ActionArguments.Values)
        {
            if (arg is null) continue;

            var vt = typeof(IValidator<>).MakeGenericType(arg.GetType());
            if (ctx.HttpContext.RequestServices.GetService(vt) is not IValidator validator) continue;

            ValidationResult res = await validator.ValidateAsync(new ValidationContext<object>(arg));
            if (!res.IsValid)
            {
                var errors = res.Errors
                    .GroupBy(e => e.PropertyName)
                    .ToDictionary(g => g.Key, g => g.Select(e => e.ErrorMessage).ToArray());

                ctx.Result = new BadRequestObjectResult(new ValidationProblemDetails(errors));
                return;
            }
        }
        await next();
    }
}