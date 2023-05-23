using AtSepete.DataAccess.Migrations;
using AtSepete.Dtos.Dto.Categories;
using AtSepete.Dtos.Dto.Markets;
using AtSepete.Dtos.Dto.Products;
using AtSepete.Entities.Data;
using AtSepete.UI.ApiResponses.CategoryApiResponse;
using AtSepete.UI.ApiResponses.MarketApiResponse;
using AtSepete.UI.ApiResponses.ProductApiResponse;
using AtSepete.UI.ApiResponses.ProductResponse;
using AtSepete.UI.Areas.Admin.Models.CategoryVMs;
using AtSepete.UI.Areas.Admin.Models.MarketVMs;
using AtSepete.UI.Areas.Admin.Models.ProductVMs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using NToastNotify;
using System.Net;
using System.Text;

namespace AtSepete.UI.Areas.Admin.Controllers
{
    public class ProductController : AdminBaseController
    {
        private readonly IMapper _mapper;

        public ProductController(IToastNotification toastNotification, IConfiguration configuration, IMapper mapper) : base(toastNotification, configuration)
        {
            _mapper = mapper;
        }

        public async Task<IActionResult> ProductList()
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", UserToken);
                using (HttpResponseMessage response = await httpClient.GetAsync($"{ApiBaseUrl}/Product/GetAllProduct"))
                {
                    if (response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        return RedirectToAction("RefreshTokenLogin", "Login", new { returnUrl = HttpContext.Request.Path, area = "" });
                    }
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    ProductListResponse productList = JsonConvert.DeserializeObject<ProductListResponse>(apiResponse);
                    if (productList.IsSuccess)
                    {
                        var markets = _mapper.Map<List<ProductListDto>, List<AdminProductListVM>>(productList.Data);
                        NotifySuccess(productList.Message);
                        return View(markets);
                    }
                    else
                    {
                        NotifyError(productList.Message);
                        return RedirectToAction("Index", "Admin");
                    }
                };

            }
        }
        [HttpGet]
        public async Task<IActionResult> AddProduct()
        {
            AdminProductCreateVM adminProductCreateVM = new() { Categories = await GetCategoriesAsync() };
            if (adminProductCreateVM.Categories is null)
            {
                return RedirectToAction("RefreshTokenLogin", "Login", new { returnUrl = HttpContext.Request.Path, area = "" });
            }
            return View(adminProductCreateVM);

        }
        [HttpPost]
        public async Task<IActionResult> AddProduct(AdminProductCreateVM adminProductCreateVM)
        {
            using (var httpClient = new HttpClient())
            {
                CreateProductDto createProductDto = _mapper.Map<AdminProductCreateVM, CreateProductDto>(adminProductCreateVM);
                MultipartFormDataContent formData = new MultipartFormDataContent();
                formData.Add(new StringContent(createProductDto.Title), "Title");
                formData.Add(new StringContent(createProductDto.Barcode), "Barcode");
                formData.Add(new StringContent(createProductDto.ProductName), "ProductName");
                formData.Add(new StringContent(createProductDto.Quantity), "Quantity");
                formData.Add(new StringContent(createProductDto.Unit), "Unit");
                formData.Add(new StringContent(createProductDto.Description), "Description");
                formData.Add(new StreamContent(createProductDto.Photo.OpenReadStream()), "Photo", createProductDto.Photo.FileName);
                formData.Add(new StringContent(createProductDto.PhotoPath ?? ""), "PhotoPath");
                formData.Add(new StringContent(createProductDto.CategoryId.ToString()), "CategoryId");
                formData.Add(new StringContent(createProductDto.CreatedDate?.ToString()), "CreatedDate");

                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", UserToken);
                //StringContent content = new StringContent(JsonConvert.SerializeObject(createProductDto), Encoding.UTF8, "application/Json");
                using (HttpResponseMessage response = await httpClient.PostAsync($"{ApiBaseUrl}/Product/AddProduct", formData))
                {
                    if (response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        return RedirectToAction("RefreshTokenLogin", "Login", new { returnUrl = HttpContext.Request.Path, area = "" });
                    }
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    AddProductResponse addedProduct = JsonConvert.DeserializeObject<AddProductResponse>(apiResponse);//burası patlıyor!!!
                    if (addedProduct.IsSuccess)
                    {
                        NotifySuccess(addedProduct.Message);
                        return RedirectToAction("ProductList");
                    }
                    else
                    {
                        NotifyError(addedProduct.Message);
                        adminProductCreateVM.Categories = await GetCategoriesAsync();
                        return View(adminProductCreateVM);
                    }
                };

            }
        }
        [HttpGet]
        public async Task<IActionResult> DetailProduct(Guid id)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", UserToken);
                using (HttpResponseMessage response = await httpClient.GetAsync($"{ApiBaseUrl}/Market/GetByIdMarket/{id}"))
                {
                    if (response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        return RedirectToAction("RefreshTokenLogin", "Login", new { returnUrl = HttpContext.Request.Path, area = "" });
                    }
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    DetailMarketResponse detailMarket = JsonConvert.DeserializeObject<DetailMarketResponse>(apiResponse);
                    if (detailMarket.IsSuccess)
                    {
                        var market = _mapper.Map<MarketDto, AdminMarketDetailVM>(detailMarket.Data);//data'ların response' den boş gelme ihtimalkeri de kontrol edilmeli
                        NotifySuccess(detailMarket.Message);
                        return View(market);
                    }
                    else
                    {
                        NotifyError(detailMarket.Message);
                        return RedirectToAction("MarketList");
                    }
                };

            }
        }
        [HttpGet]
        public async Task<IActionResult> UpdateProduct(Guid id)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", UserToken);
                using (HttpResponseMessage response = await httpClient.GetAsync($"{ApiBaseUrl}/Market/GetByIdMarket/{id}"))
                {
                    if (response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        return RedirectToAction("RefreshTokenLogin", "Login", new { returnUrl = HttpContext.Request.Path, area = "" });
                    }
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    DetailMarketResponse updateMarket = JsonConvert.DeserializeObject<DetailMarketResponse>(apiResponse);
                    if (updateMarket.IsSuccess)
                    {
                        var market = _mapper.Map<MarketDto, AdminMarketUpdateVM>(updateMarket.Data);//data'ların response' den boş gelme ihtimalkeri de kontrol edilmeli
                        NotifySuccess(updateMarket.Message);
                        return View(market);
                    }
                    else
                    {
                        NotifyError(updateMarket.Message);
                        return RedirectToAction("MarketList");
                    }
                };

            }
        }
        [HttpPost]
        public async Task<IActionResult> UpdateProduct(AdminMarketUpdateVM adminMarketUpdateVM)
        {
            using (var httpClient = new HttpClient())
            {
                var updateMarketDto = _mapper.Map<AdminMarketUpdateVM, UpdateMarketDto>(adminMarketUpdateVM);

                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", UserToken);
                StringContent content = new StringContent(JsonConvert.SerializeObject(updateMarketDto), Encoding.UTF8, "application/Json");
                using (HttpResponseMessage response = await httpClient.PutAsync($"{ApiBaseUrl}/Market/UpdateMarket/{adminMarketUpdateVM.Id}", content))
                {
                    if (response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        return RedirectToAction("RefreshTokenLogin", "Login", new { returnUrl = HttpContext.Request.Path, area = "" });
                    }
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    UpdateMarketResponse updateMarket = JsonConvert.DeserializeObject<UpdateMarketResponse>(apiResponse);
                    if (updateMarket.IsSuccess)
                    {
                        NotifySuccess(updateMarket.Message);
                        return RedirectToAction("MarketList");
                    }
                    else
                    {
                        NotifyError(updateMarket.Message);
                        return RedirectToAction("UpdateMarket", new { id = adminMarketUpdateVM.Id });
                    }
                };

            }
        }
        [HttpGet]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", UserToken);
                using (HttpResponseMessage response = await httpClient.DeleteAsync($"{ApiBaseUrl}/Market/SoftDeleteMarket/{id}"))
                {
                    if (response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        return RedirectToAction("RefreshTokenLogin", "Login", new { returnUrl = HttpContext.Request.Path, area = "" });
                    }
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    DeleteMarketResponse deletedMarket = JsonConvert.DeserializeObject<DeleteMarketResponse>(apiResponse);

                    return Json(deletedMarket);
                };

            };


        }
        private async Task<SelectList?> GetCategoriesAsync(Guid? categoryId = null)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", UserToken);
                using (HttpResponseMessage response = await httpClient.GetAsync($"{ApiBaseUrl}/Category/GetAllCategory"))
                {

                    string apiResponse = await response.Content.ReadAsStringAsync();
                    CategoryListResponse categoryList = JsonConvert.DeserializeObject<CategoryListResponse>(apiResponse);
                    if (categoryList is not null)
                    {
                        var categories = _mapper.Map<List<CategoryListDto>, List<AdminCategoryListVM>>(categoryList.Data);
                        return new SelectList(categories.Select(x => new SelectListItem
                        {
                            Selected = x.Id == (categoryId != null ? categoryId.Value : categoryId),
                            Value = x.Id.ToString(),
                            Text = x.Name
                        }).OrderBy(x => x.Text), "Value", "Text");
                    }

                    return null;

                };

            }


        }
    }
}
