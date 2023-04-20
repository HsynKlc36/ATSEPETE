using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AtSepete.Api.Controllers.Customer
{
    [Route("AtSepeteApi/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Customer")]
    public class CustomerController : ControllerBase
    {
    }
}
