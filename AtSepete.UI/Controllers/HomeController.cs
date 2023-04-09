using AtSepete.Dtos.Dto.Categories;
using AtSepete.Dtos.Dto.Users;
using AtSepete.Entities.BaseData;
using AtSepete.Entities.Data;
using AtSepete.Repositories.Abstract;
using AtSepete.Results;
using AtSepete.Results.Concrete;
using AtSepete.UI.ApiResponses;
using AtSepete.UI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text;

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

            HttpClient httpClient = new HttpClient();
            HttpResponseMessage response = await httpClient.GetAsync($"https://localhost:7286/AtSepeteApi/user/GetByIdUser/f6c16176-471e-4ef5-1c67-08db38735e6e");
            string apiResponse = await response.Content.ReadAsStringAsync();
            UserApiResponse user = JsonConvert.DeserializeObject<UserApiResponse>(apiResponse);           
            return View(user);

        }
        [HttpGet]
        public IActionResult AddCategory()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddCategory(Category category )
        {
            CreateCategoryDto dto = new CreateCategoryDto();
            dto.Description = category.Description;
            dto.Name = category.Name;
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(dto), Encoding.UTF8, "application/Json");
                using (var answer = await httpClient.PostAsync("https://localhost:7286/AtSepeteApi/Category/AddCategory", content))
                {
                    string apiAnswer = await answer.Content.ReadAsStringAsync();
                    AddCategoryResponse categoryResponse = JsonConvert.DeserializeObject<AddCategoryResponse>(apiAnswer);
                    ViewBag.cevap = categoryResponse.Message; 
                }
            }
           
            return View();
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
