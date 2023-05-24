using Microsoft.AspNetCore.Mvc;

namespace AtSepete.UI.Areas.Admin.Controllers
{
    public class ProductMarketController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
