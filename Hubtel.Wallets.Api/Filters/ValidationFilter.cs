using System.Collections.Generic;
using Hubtel.Wallets.Api.Contracts.ResponseDtos;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Hubtel.Wallets.Api.Filters
{
    [AttributeUsage(
        validOn: AttributeTargets.Method | AttributeTargets.Parameter,
        AllowMultiple = true
    )]
    public class ValidateAttribute<T> : ActionFilterAttribute
    {
        private readonly string _argumentValue;

        public ValidateAttribute(string argumentValue)
        {
            this._argumentValue = argumentValue;
        }

        public async override Task OnActionExecutionAsync(
            ActionExecutingContext context,
            ActionExecutionDelegate next
        )
        {
            IDictionary<string, object?> dict = context.ActionArguments;
            object validateValue;
            bool isValidParameterKey = dict.TryGetValue(_argumentValue, out validateValue!);
            if (isValidParameterKey)
            {
                if (validateValue != null)
                {
                    var validator =
                        (IValidator<T>)
                            context.HttpContext.RequestServices.GetRequiredService<IValidator<T>>();

                    var obj = new ValidationContext<object>(validateValue);
                    var result = await validator.ValidateAsync(obj);
                    if (!result.IsValid)
                    {
                        var errors = new Dictionary<string, string>();
                        foreach (var error in result.Errors)
                        {
                            var key = string.IsNullOrEmpty(error.PropertyName)
                                ? _argumentValue
                                : error.PropertyName;
                            errors[key] = error.ErrorMessage;
                        }

                        var response = new ResponseWithErrors<Dictionary<string, string>?>()
                        {
                            statusCode = 400,
                            errors = errors,
                        };

                        context.Result = new BadRequestObjectResult(response);
                        return;
                    }
                }
                else
                {
                    var response = new ResponseWithError()
                    {
                        statusCode = 400,
                        message = _argumentValue + "requires a valid value",
                    };

                    context.Result = new BadRequestObjectResult(response);
                    return;
                }
            }
            else
            {
                var response = new ResponseWithError()
                {
                    statusCode = 400,
                    message = _argumentValue + "is not a valid parameter value",
                };

                context.Result = new BadRequestObjectResult(response);
                return;
            }

            await next();
        }
    }
}
