﻿using AtSepete.Dtos.Dto.Categories;
using AtSepete.Dtos.Dto.Users;
using AtSepete.Entities.BaseData;
using AtSepete.Entities.Data;
using AtSepete.Repositories.Abstract;
using AtSepete.Results;
using AtSepete.Results.Concrete;
using AtSepete.UI.ApiResponses;
using AtSepete.UI.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
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
            //token'ı headers'ta taşımak için gerekli
            HttpContext.Request.Headers.Add("Authorization", "Bearer " + "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJJRCI6IjQ2YjQ0YjEyLWFmZGItNGE0Ni1mMmIwLTA4ZGIzZThhZGJhMiIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL25hbWUiOiJodXNleWluIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvc3VybmFtZSI6ImtsYyIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL2VtYWlsYWRkcmVzcyI6Imh1c2V5aW5fa2lsaWM5NkBob3RtYWlsLmNvbSIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6IkN1c3RvbWVyIiwibmJmIjoxNjgzMjIxODgxLCJleHAiOjE2ODMyMjM2ODEsImlzcyI6Ind3dy5teWFwaS5jb20iLCJhdWQiOiJ3d3cuYXRzZXBldGUuY29tIn0.TJUqnQ0UttHfq1ns0ttoMoMCNazJPQ9hVXAT-nZvUcs");
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJJRCI6IjQ2YjQ0YjEyLWFmZGItNGE0Ni1mMmIwLTA4ZGIzZThhZGJhMiIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL25hbWUiOiJodXNleWluIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvc3VybmFtZSI6ImtsYyIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL2VtYWlsYWRkcmVzcyI6Imh1c2V5aW5fa2lsaWM5NkBob3RtYWlsLmNvbSIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6IkN1c3RvbWVyIiwibmJmIjoxNjgzMjIxODgxLCJleHAiOjE2ODMyMjM2ODEsImlzcyI6Ind3dy5teWFwaS5jb20iLCJhdWQiOiJ3d3cuYXRzZXBldGUuY29tIn0.TJUqnQ0UttHfq1ns0ttoMoMCNazJPQ9hVXAT-nZvUcs");
            HttpResponseMessage response = await httpClient.GetAsync($"https://localhost:7286/AtSepeteApi/user/GetByIdUser/46b44b12-afdb-4a46-f2b0-08db3e8adba2");
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
        [Authorize(Roles ="Customer")]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public IActionResult NewPassword(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token); 
            Claim emailClaim = jwtToken.Claims.FirstOrDefault(c => c.Type ==ClaimTypes.Email); 
            if (emailClaim != null) 
            { string emailValue = emailClaim.Value; }

            return View();
        }
    }

}
