using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using devdev_api.Common;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace devdev_api.Filters
{
    public class ValidationFilter : IAsyncActionFilter
    {
        private readonly IServiceProvider _serviceProvider;

        public ValidationFilter(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var errors = new List<string>();

            foreach (var arg in context.ActionArguments.Values)
            {
                if (arg == null) continue;

                var validatorType = typeof(IValidator<>).MakeGenericType(arg.GetType());
                var validator = _serviceProvider.GetService(validatorType);

                if (validator is not null)
                {
                    var method = validatorType.GetMethod("ValidateAsync", new[] { arg.GetType(), typeof(CancellationToken) });

                    if (method != null)
                    {
                        var result = await (Task<FluentValidation.Results.ValidationResult>)
                            method.Invoke(validator, new object[] { arg, context.HttpContext.RequestAborted })!;

                        if (!result.IsValid)
                        {
                            errors.AddRange(result.Errors.Select(e => e.ErrorMessage));
                        }
                    }
                }
            }

            if (errors.Any())
            {
                context.Result = new BadRequestObjectResult(
                    ApiResponse<object>.Fail("Validation failed.", errors));
                return;
            }

            await next();
        }
    }
}