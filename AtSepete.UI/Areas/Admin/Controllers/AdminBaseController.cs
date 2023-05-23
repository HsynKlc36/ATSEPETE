using AtSepete.UI.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;

namespace AtSepete.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminBaseController:BaseController
    {
        public AdminBaseController(IToastNotification toastNotification,IConfiguration configuration) : base(toastNotification,configuration)
        {

        }

    }
}
