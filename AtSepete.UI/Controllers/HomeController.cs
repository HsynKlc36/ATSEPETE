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
     
        private readonly IMapper _mapper;

        public HomeController(IMapper mapper)
        {
           
            _mapper = mapper;
        }
        //user dan get işlemi ile veri getirme denendi
        public async Task<IActionResult> Index()
        {

            HttpClient httpClient = new HttpClient();
            HttpResponseMessage response = await httpClient.GetAsync($"https://localhost:7286/AtSepeteApi/user/GetByIdUser/df035792-e647-45bb-cbff-08db3cf9a924");
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
        //post işlemi ile category ekleme işlemi ve dönen sonucu yakalayıp view da gösterme denendi!!
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

        [HttpGet]
        public async Task<IActionResult> UpdateCategory()
        {

            HttpClient httpClient = new HttpClient();
            HttpResponseMessage response = await httpClient.GetAsync($"https://localhost:7286/AtSepeteApi/Category/GetByIdCategory/ef43c461-6948-4939-cc3b-08db393cda8a");
            string apiResponse = await response.Content.ReadAsStringAsync();
            AddCategoryResponse category = JsonConvert.DeserializeObject<AddCategoryResponse>(apiResponse);
            return View(category);
        }
        [HttpPost]
        //post işlemi ile category ekleme işlemi ve dönen sonucu yakalayıp view da gösterme denendi!!
        public async Task<IActionResult> UpdateCategory(AddCategoryResponse category)
        {

            CreateCategoryDto dto = new CreateCategoryDto();
            var updateCategoryDto=_mapper.Map( category.Data ,dto);
          
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(updateCategoryDto), Encoding.UTF8, "application/Json");
                using (var answer = await httpClient.PutAsync("https://localhost:7286/AtSepeteApi/Category/UpdateCategory/ef43c461-6948-4939-cc3b-08db393cda8a", content))
                {
                    string apiAnswer = await answer.Content.ReadAsStringAsync();
                    AddCategoryResponse categoryResponse = JsonConvert.DeserializeObject<AddCategoryResponse>(apiAnswer);
                    ViewBag.cevap = categoryResponse.Message;
                }
            }

            return View();
        }
        [HttpGet]
        public async Task<IActionResult> DeleteUser(Guid id)
        {

            HttpClient httpClient = new HttpClient();
            HttpResponseMessage response = await httpClient.DeleteAsync($"https://localhost:7286/AtSepeteApi/user/SoftDeleteUser/{id}");
            string apiResponse = await response.Content.ReadAsStringAsync();
            UserApiResponse user = JsonConvert.DeserializeObject<UserApiResponse>(apiResponse);
            //return RedirectToAction("AddCategory");
            return Json(user);

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
