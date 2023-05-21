using AtSepete.Dtos.Dto.Categories;
using AtSepete.Dtos.Dto.Users;
using AtSepete.Entities.Data;
using AtSepete.UI.ApiResponses.CategoryApiResponse;
using AtSepete.UI.ApiResponses.UserApiResponse;
using AtSepete.UI.Areas.Admin.Models.AdminVMs;
using AtSepete.UI.Areas.Admin.Models.CategoryVMs;
using AtSepete.UI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NToastNotify;
using System.Net;
using System.Text;

namespace AtSepete.UI.Areas.Admin.Controllers
{
    public class CategoryController : AdminBaseController
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
                    if (categoryList.IsSuccess)
                    {
                        var categories = _mapper.Map<List<CategoryListDto>, List<AdminCategoryListVM>>(categoryList.Data);
                        NotifySuccess(categoryList.Message);
                        return View(categories);
                    }
                    else
                    {
                        NotifyError(categoryList.Message);
                        return RedirectToAction("Index", "Admin");
                    }
                };

            }
        }
        [HttpGet]
        public async Task<IActionResult> AddCategory()
        {
           return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddCategory(AdminCategoryCreateVM adminCategoryCreateVM)
        {
            using (var httpClient = new HttpClient())
            {
            CreateCategoryDto createCategoryDto = _mapper.Map<AdminCategoryCreateVM,CreateCategoryDto>(adminCategoryCreateVM);
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", UserToken);
                StringContent content = new StringContent(JsonConvert.SerializeObject(createCategoryDto), Encoding.UTF8, "application/Json");
                using (HttpResponseMessage response = await httpClient.PostAsync($"https://localhost:7286/AtSepeteApi/Category/AddCategory",content))
                {
                    if (response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        return RedirectToAction("RefreshTokenLogin", "Login", new { returnUrl = HttpContext.Request.Path });
                    }
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    AddCategoryResponse addedCategory = JsonConvert.DeserializeObject<AddCategoryResponse>(apiResponse);
                    if (addedCategory.IsSuccess)
                    {
                        NotifySuccess(addedCategory.Message);
                        return RedirectToAction("CategoryList");  
                    }
                    else
                    {
                        NotifyError(addedCategory.Message);
                        return View(adminCategoryCreateVM);
                    }
                };

            }
        }

        [HttpGet]
        public async Task<IActionResult> UpdateCategory(Guid id)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", UserToken);
                using (HttpResponseMessage response = await httpClient.GetAsync($"https://localhost:7286/AtSepeteApi/Category/GetByIdCategory/{id}"))
                {
                    if (response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        return RedirectToAction("RefreshTokenLogin", "Login", new { returnUrl = HttpContext.Request.Path });
                    }
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    UpdateCategoryResponse updateCategory = JsonConvert.DeserializeObject<UpdateCategoryResponse>(apiResponse);
                    if (updateCategory.IsSuccess)
                    {
                        var category = _mapper.Map<CategoryDto, AdminCategoryUpdateVM>(updateCategory.Data);//data'ların response' den boş gelme ihtimalkeri de kontrol edilmeli
                        NotifySuccess(updateCategory.Message);
                        return View(category);
                    }
                    else
                    {
                        NotifyError(updateCategory.Message);
                        return RedirectToAction("CategoryList");
                    }
                };

            }
        }
        [HttpPost]
        public async Task<IActionResult> UpdateCategory(AdminCategoryUpdateVM adminCategoryUpdateVM)
        {
            using (var httpClient = new HttpClient())
            {
                var updateCategoryDto = _mapper.Map<AdminCategoryUpdateVM, UpdateCategoryDto>(adminCategoryUpdateVM);


                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", UserToken);
                StringContent content = new StringContent(JsonConvert.SerializeObject(updateCategoryDto), Encoding.UTF8, "application/Json");
                using (HttpResponseMessage response = await httpClient.PutAsync($"https://localhost:7286/AtSepeteApi/Category/UpdateCategory/{adminCategoryUpdateVM.Id}", content))
                {
                    if (response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        return RedirectToAction("RefreshTokenLogin", "Login", new { returnUrl = HttpContext.Request.Path });
                    }
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    UpdateCategoryResponse updateCategory = JsonConvert.DeserializeObject<UpdateCategoryResponse>(apiResponse);
                    if (updateCategory.IsSuccess)
                    {
                        NotifySuccess(updateCategory.Message);
                        return RedirectToAction("CategoryList");
                    }
                    else
                    {
                        NotifyError(updateCategory.Message);
                        return RedirectToAction("UpdateCategory",new{ id=adminCategoryUpdateVM.Id });
                    }
                };

            }
        }
        [HttpGet]
        public async Task<IActionResult> DeleteCategory(Guid id)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", UserToken);
                using (HttpResponseMessage response = await httpClient.DeleteAsync($"https://localhost:7286/AtSepeteApi/Category/SoftDeleteCategory/{id}"))
                {
                    if (response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        return RedirectToAction("RefreshTokenLogin", "Login", new { returnUrl = HttpContext.Request.Path });
                    }
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    DeleteCategoryResponse deletedCategory = JsonConvert.DeserializeObject<DeleteCategoryResponse>(apiResponse);

                    return Json(deletedCategory);
                };

            };


        }

    }
}
