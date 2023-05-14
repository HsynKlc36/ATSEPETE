using AtSepete.Dtos.Dto.Categories;
using AtSepete.Dtos.Dto.Users;
using AtSepete.Entities.BaseData;
using AtSepete.Entities.Data;
using AtSepete.Repositories.Abstract;
using AtSepete.Results;
using AtSepete.Results.Concrete;
using AtSepete.UI.ApiResponses.CategoryApiResponse;
using AtSepete.UI.Areas.Admin.Controllers;
using AtSepete.UI.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Mime;
using System.Security.Claims;
using System.Text;

namespace AtSepete.UI.Controllers
{
    public class HomeController : BaseController
    {

        private readonly IMapper _mapper;

        public HomeController(IMapper mapper)
        {

            _mapper = mapper;
        }
        //user dan get işlemi ile veri getirme denendi
        [Authorize(Roles ="Admin,Customer")] 
        public async Task<IActionResult> Index()
        {
            
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", UserToken);
            HttpResponseMessage response = await httpClient.GetAsync($"https://localhost:7286/AtSepeteApi/user/GetByIdUser/46b44b12-afdb-4a46-f2b0-08db3e8adba2");
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                return RedirectToAction("RefreshTokenLogin", "Login",new { returnUrl = HttpContext.Request.Path });
            }
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
        public async Task<IActionResult> AddCategory(Category category)
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
            var updateCategoryDto = _mapper.Map(category.Data, dto);

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
        [Authorize(Roles = "Customer,Admin")]
        public IActionResult Privacy()
        {
            return View();
        }

  
    }

}
