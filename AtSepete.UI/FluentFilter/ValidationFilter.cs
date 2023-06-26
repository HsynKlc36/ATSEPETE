using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace AtSepete.UI.FluentFilter
{
    public class ValidationFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                var errors=context.ModelState
                    .Where(x=>x.Value.Errors.Any())
                    .ToDictionary(e=>e.Key,e=>e.Value.Errors.Select(e=>e.ErrorMessage))
                    .ToArray();

                context.Result = new ViewResult
                {
                    ViewName = context.ActionDescriptor.RouteValues["action"],
                    ViewData = new ViewDataDictionary(new EmptyModelMetadataProvider(), context.ModelState)
                    {
                        Model = context.ActionArguments.FirstOrDefault().Value
                    }
                };
                return;
            }
            await next();
        }
    }
}
