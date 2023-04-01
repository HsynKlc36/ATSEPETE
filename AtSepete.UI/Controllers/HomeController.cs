using AtSepete.Entities.BaseData;
using AtSepete.Entities.Data;
using AtSepete.Repositories.Abstract;
using AtSepete.UI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace AtSepete.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;


        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
           
        }

        public async Task<IActionResult> Index()
        {
            var products=new Product();
            using (var httpClient = new HttpClient())//api ile localhosttan bağlantı kurarak istekleri yönetir.
            {
                using (var answer = await httpClient.GetAsync("https://localhost:7286/AtSepeteApi/Product/GetAllProduct"))
                {
                    string apiAnswer = await answer.Content.ReadAsStringAsync();
                    products = JsonConvert.DeserializeObject<Product>(apiAnswer);
                }
            }
            return View(products);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}