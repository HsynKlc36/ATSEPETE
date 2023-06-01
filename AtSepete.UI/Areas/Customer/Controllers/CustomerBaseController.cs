using AtSepete.UI.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;

namespace AtSepete.UI.Areas.Customer.Controllers
{
    [Area("Customer")]
    //[Authorize(Roles = "Customer")]
    public class CustomerBaseController : BaseController
    {
        public CustomerBaseController(IToastNotification toastNotification, IConfiguration configuration) : base(toastNotification, configuration)
        {

        }
    
    }
}
