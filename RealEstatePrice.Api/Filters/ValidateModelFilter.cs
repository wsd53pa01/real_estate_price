using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using RealEstatePrice.Core.Wrappers;

namespace RealEstatePrice.Api.Filters
{
    public class ValidateModelFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                var errorsInModelState = context.ModelState
                        .Where(x => x.Value.Errors.Count > 0)
                        .ToDictionary(k => k.Key, k => k.Value.Errors.Select(x => x.ErrorMessage)).ToArray();
                var errors = errorsInModelState.Select(x => new { FieldName = x.Key, Message = x.Value }).ToList();
                Response<object> response = new Response<object>(errors, "Model Invalid", StatusCodeOptions.E001, false);
                context.Result = new JsonResult(response);
                BadRequestResult badRequestResult = new BadRequestResult();
                badRequestResult.ExecuteResult(context);
                return ;
            }
            await next();
        }
    }
}