using AtSepete.DataAccess.Migrations;
using AtSepete.Dtos.Dto.Categories;
using AtSepete.Dtos.Dto.Products;
using AtSepete.Dtos.Dto.Products;
using AtSepete.Entities.Data;
using AtSepete.UI.ApiResponses.CategoryApiResponse;
using AtSepete.UI.ApiResponses.ProductApiResponse;
using AtSepete.UI.ApiResponses.ProductApiResponse;
using AtSepete.UI.ApiResponses.ProductResponse;
using AtSepete.UI.Areas.Admin.Models.CategoryVMs;
using AtSepete.UI.Areas.Admin.Models.ProductVMs;
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
        [HttpGet]
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
                        var Products = _mapper.Map<List<ProductListDto>, List<AdminProductListVM>>(productList.Data);
                        NotifySuccess(productList.Message);
                        return View(Products);
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
        public async Task<IActionResult> DetailProduct(Guid id)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", UserToken);
                using (HttpResponseMessage response = await httpClient.GetAsync($"{ApiBaseUrl}/Product/GetByIdProduct/{id}"))
                {
                    if (response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        return RedirectToAction("RefreshTokenLogin", "Login", new { returnUrl = HttpContext.Request.Path, area = "" });
                    }
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    DetailProductResponse detailProduct = JsonConvert.DeserializeObject<DetailProductResponse>(apiResponse);
                    if (detailProduct.IsSuccess)
                    {
                        var Product = _mapper.Map<ProductDto, AdminProductDetailVM>(detailProduct.Data);//data'ların response' den boş gelme ihtimalkeri de kontrol edilmeli
                        NotifySuccess(detailProduct.Message);
                        return View(Product);
                    }
                    else
                    {
                        NotifyError(detailProduct.Message);
                        return RedirectToAction("ProductList");
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
                if (adminProductCreateVM.Photo is not null)
                {
                    byte[]? resimBytes = null; // Byte dizisi olarak almak için
                    string? resimBase64 = null; // Base64 encoded string olarak almak için
                    using (var memoryStream = new MemoryStream())
                    {
                        adminProductCreateVM.Photo.CopyTo(memoryStream);
                        resimBytes = memoryStream.ToArray();
                        // Byte dizisini string olarak dönüştürmek için
                        resimBase64 = Convert.ToBase64String(resimBytes);
                        createProductDto.PhotoFileName = resimBase64;
                    }
                }
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", UserToken);
                StringContent content = new StringContent(JsonConvert.SerializeObject(createProductDto), Encoding.UTF8, "application/Json");
                using (HttpResponseMessage response = await httpClient.PostAsync($"{ApiBaseUrl}/Product/AddProduct", content))
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
        public async Task<IActionResult> UpdateProduct(Guid id)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", UserToken);
                using (HttpResponseMessage response = await httpClient.GetAsync($"{ApiBaseUrl}/Product/GetByIdProduct/{id}"))
                {
                    if (response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        return RedirectToAction("RefreshTokenLogin", "Login", new { returnUrl = HttpContext.Request.Path, area = "" });
                    }
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    DetailProductResponse updateProduct = JsonConvert.DeserializeObject<DetailProductResponse>(apiResponse);
                    if (updateProduct.IsSuccess)
                    {
                        var product = _mapper.Map<ProductDto, AdminProductUpdateVM>(updateProduct.Data);
                        product.Categories = await GetCategoriesAsync(product.CategoryId);
                        NotifySuccess(updateProduct.Message);
                        return View(product);
                    }
                    else
                    {
                        NotifyError(updateProduct.Message);
                        return RedirectToAction("ProductList");
                    }
                };
            }
        }
        [HttpPost]
        public async Task<IActionResult> UpdateProduct(AdminProductUpdateVM adminProductUpdateVM)
        {
            using (var httpClient = new HttpClient())
            {
                var updateProductDto = _mapper.Map<AdminProductUpdateVM, UpdateProductDto>(adminProductUpdateVM);
                if (adminProductUpdateVM.Photo is not null)
                {
                    byte[]? resimBytes = null; // Byte dizisi olarak almak için
                    string? resimBase64 = null; // Base64 encoded string olarak almak için
                    using (var memoryStream = new MemoryStream())
                    {
                        adminProductUpdateVM.Photo.CopyTo(memoryStream);
                        resimBytes = memoryStream.ToArray();
                        // Byte dizisini string olarak dönüştürmek için
                        resimBase64 = Convert.ToBase64String(resimBytes);
                        updateProductDto.PhotoFileName = resimBase64;
                    }
                }

                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", UserToken);
                StringContent content = new StringContent(JsonConvert.SerializeObject(updateProductDto), Encoding.UTF8, "application/Json");
                using (HttpResponseMessage response = await httpClient.PutAsync($"{ApiBaseUrl}/Product/UpdateProduct/{adminProductUpdateVM.Id}", content))
                {
                    if (response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        return RedirectToAction("RefreshTokenLogin", "Login", new { returnUrl = HttpContext.Request.Path, area = "" });
                    }
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    UpdateProductResponse updateProduct = JsonConvert.DeserializeObject<UpdateProductResponse>(apiResponse);
                    if (updateProduct.IsSuccess)
                    {
                        NotifySuccess(updateProduct.Message);
                        return RedirectToAction("ProductList");
                    }
                    else
                    {
                        NotifyError(updateProduct.Message);
                        adminProductUpdateVM.Categories = await GetCategoriesAsync(adminProductUpdateVM.CategoryId);
                        return View(adminProductUpdateVM);
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
                using (HttpResponseMessage response = await httpClient.DeleteAsync($"{ApiBaseUrl}/Product/SoftDeleteProduct/{id}"))
                {
                    if (response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        return RedirectToAction("RefreshTokenLogin", "Login", new { returnUrl = HttpContext.Request.Path, area = "" });
                    }
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    DeleteProductResponse deletedProduct = JsonConvert.DeserializeObject<DeleteProductResponse>(apiResponse);
                    return Json(deletedProduct);
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
