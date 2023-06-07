using Microsoft.AspNetCore.Mvc;
using NToastNotify;

namespace AtSepete.UI.Areas.Customer.Controllers
{
    public class CustomerController : CustomerBaseController
    {
        public CustomerController(IToastNotification toastNotification, IConfiguration configuration) : base(toastNotification, configuration)
        {

        }
        public IActionResult Index()
        {
            return RedirectToAction("HomePage", "Shop");
        }


    }
}
