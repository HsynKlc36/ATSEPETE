using AtSepete.Results.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace AtSepete.Api.Controllers
{
    [AllowAnonymous]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorsController : ControllerBase
    {
        [Route("error")]
        public ErrorDataResult<string> Error()
        {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
            return new ErrorDataResult<string>(context.Error.Message);
        }
    }
}
