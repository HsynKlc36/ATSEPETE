using AtSepete.Dtos.Dto.Categories;
using AtSepete.Dtos.Dto.Markets;
using AtSepete.Dtos.Dto.ProductMarkets;
using AtSepete.Dtos.Dto.Products;
using AtSepete.UI.ApiResponses.CategoryApiResponse;
using AtSepete.UI.ApiResponses.MarketApiResponse;
using AtSepete.UI.ApiResponses.ProductApiResponse;
using AtSepete.UI.ApiResponses.ProductMarketApiResponse;
using AtSepete.UI.ApiResponses.ProductResponse;
using AtSepete.UI.Areas.Admin.Models.CategoryVMs;
using AtSepete.UI.Areas.Admin.Models.MarketVMs;
using AtSepete.UI.Areas.Admin.Models.ProductMarketVMs;
using AtSepete.UI.Areas.Admin.Models.ProductVMs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using NToastNotify;
using System.Net;
using System.Text;

namespace AtSepete.UI.Areas.Admin.Controllers
{
    public class ProductMarketController : AdminBaseController
    {
        private readonly IMapper _mapper;
        public ProductMarketController(IToastNotification toastNotification, IConfiguration configuration, IMapper mapper) : base(toastNotification, configuration)
        {
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> ProductMarketList()
        {
            //burada listeleme yaparken ürün isimleri ve market isimleri yazılacaktır.id'leri değil isimleri görünecek
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", UserToken);
                using (HttpResponseMessage response = await httpClient.GetAsync($"{ApiBaseUrl}/ProductMarket/GetAllProductMarkets"))
                {
                    if (response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        return RedirectToAction("RefreshTokenLogin", "Login", new { returnUrl = HttpContext.Request.Path, area = "" });
                    }
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    ProductMarketListResponse productMarketList = JsonConvert.DeserializeObject<ProductMarketListResponse>(apiResponse);
                    if (productMarketList.IsSuccess)
                    {
                        var ProductMarkets = _mapper.Map<List<ProductMarketListDto>, List<AdminProductMarketListVM>>(productMarketList.Data); 
                        NotifySuccess(productMarketList.Message);
                        return View(ProductMarkets);
                    }
                    else
                    {
                        NotifyError(productMarketList.Message);
                        return RedirectToAction("Index", "Admin");
                    }
                };

            }
        }
        [HttpGet]
        public async Task<IActionResult> DetailProductMarket(Guid id)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", UserToken);
                using (HttpResponseMessage response = await httpClient.GetAsync($"{ApiBaseUrl}/ProductMarket/GetByIdProductMarket/{id}"))
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
        public async Task<IActionResult> AddProductMarket()
        {
            AdminProductMarketCreateVM adminProductMarketCreateVM = new() {  Products = await GetProductsAsync(), Markets= await GetMarketsAsync() };
            if (adminProductMarketCreateVM.Markets is null || adminProductMarketCreateVM.Products is null)
            {
                return RedirectToAction("RefreshTokenLogin", "Login", new { returnUrl = HttpContext.Request.Path, area = "" });
            }
            return View(adminProductMarketCreateVM);

        }
        [HttpPost]
        public async Task<IActionResult> AddProductMarket(AdminProductMarketCreateVM adminProductMarketCreateVM)
        {
            using (var httpClient = new HttpClient())
            {
                CreateProductMarketDto createProductMarketDto = _mapper.Map<AdminProductMarketCreateVM, CreateProductMarketDto>(adminProductMarketCreateVM);             
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", UserToken);
                StringContent content = new StringContent(JsonConvert.SerializeObject(createProductMarketDto), Encoding.UTF8, "application/Json");
                using (HttpResponseMessage response = await httpClient.PostAsync($"{ApiBaseUrl}/ProductMarket/AddProductMarket", content))
                {
                    if (response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        return RedirectToAction("RefreshTokenLogin", "Login", new { returnUrl = HttpContext.Request.Path, area = "" });
                    }
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    AddProductMarketResponse addedProductMarket = JsonConvert.DeserializeObject<AddProductMarketResponse>(apiResponse);//burası patlıyor!!!
                    if (addedProductMarket.IsSuccess)
                    {
                        NotifySuccess(addedProductMarket.Message);
                        return RedirectToAction("ProductMarketList");
                    }
                    else
                    {
                        NotifyError(addedProductMarket.Message);
                        adminProductMarketCreateVM.Products = await GetProductsAsync(adminProductMarketCreateVM.ProductId);
                        adminProductMarketCreateVM.Markets = await GetMarketsAsync(adminProductMarketCreateVM.MarketId);
                        return View(adminProductMarketCreateVM);
                    }
                };
            }
        }
        private async Task<SelectList?> GetProductsAsync(Guid? productId = null)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", UserToken);
                using (HttpResponseMessage response = await httpClient.GetAsync($"{ApiBaseUrl}/Product/GetAllProduct"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    ProductListResponse productList = JsonConvert.DeserializeObject<ProductListResponse>(apiResponse);
                    if (productList is not null)
                    {
                        var categories = _mapper.Map<List<ProductListDto>, List<AdminProductListVM>>(productList.Data);
                        return new SelectList(categories.Select(x => new SelectListItem
                        {
                            Selected = x.Id == (productId != null ? productId.Value : productId),
                            Value = x.Id.ToString(),
                            Text =  x.Title.ToUpper() +" "+ x.ProductName + " " + x.Quantity + " " + x.Unit 
                        }).OrderBy(x => x.Text), "Value", "Text");
                    }
                    return null;
                };
            }
        }
        private async Task<SelectList?> GetMarketsAsync(Guid? marketId = null)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", UserToken);
                using (HttpResponseMessage response = await httpClient.GetAsync($"{ApiBaseUrl}/Market/GetAllMarket"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    MarketListResponse marketList = JsonConvert.DeserializeObject<MarketListResponse>(apiResponse);
                    if (marketList is not null)
                    {
                        var markets = _mapper.Map<List<MarketListDto>, List<AdminMarketListVM>>(marketList.Data);
                        return new SelectList(markets.Select(x => new SelectListItem
                        {
                            Selected = x.Id == (marketId != null ? marketId.Value : marketId),
                            Value = x.Id.ToString(),
                            Text = x.MarketName
                        }).OrderBy(x => x.Text), "Value", "Text");
                    }
                    return null;
                };
            }
        }

    }
}
