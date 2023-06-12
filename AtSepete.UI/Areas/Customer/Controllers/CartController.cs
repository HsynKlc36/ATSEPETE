using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using NToastNotify;

namespace AtSepete.UI.Areas.Customer.Controllers
{
    public class CartController : CustomerBaseController
    {
        public CartController(IToastNotification toastNotification, IConfiguration configuration) : base(toastNotification, configuration)
        {

        }
        public async Task<IActionResult> ShoppingCartPage()
        {
            return View();
        }
    }
}
