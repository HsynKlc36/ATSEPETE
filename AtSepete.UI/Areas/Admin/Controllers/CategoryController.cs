using AtSepete.Dtos.Dto.Categories;
using AtSepete.Dtos.Dto.Users;
using AtSepete.UI.ApiResponses.CategoryApiResponse;
using AtSepete.UI.ApiResponses.UserApiResponse;
using AtSepete.UI.Areas.Admin.Models.AdminVMs;
using AtSepete.UI.Areas.Admin.Models.CategoryVMs;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NToastNotify;
using System.Net;

namespace AtSepete.UI.Areas.Admin.Controllers
{
    public class CategoryController :AdminBaseController
    {
        private readonly IMapper _mapper;

        public CategoryController(IToastNotification toastNotification, IMapper mapper) : base(toastNotification)
        {
            _mapper = mapper;

        }
        [HttpGet]
        public async Task<IActionResult> CategoryList()
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", UserToken);
                using (HttpResponseMessage response = await httpClient.GetAsync($"https://localhost:7286/AtSepeteApi/Category/GetAllCategory"))
                {
                    if (response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        return RedirectToAction("RefreshTokenLogin", "Login", new { returnUrl = HttpContext.Request.Path });
                    }
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    CategoryListResponse categoryList = JsonConvert.DeserializeObject<CategoryListResponse>(apiResponse);
                    var categories = _mapper.Map<List<CategoryListDto>,List<AdminCategoryListVM>>(categoryList.Data);
                    return View(categories);
                };

            }
        }
    }
}
