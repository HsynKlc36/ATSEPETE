using AtSepete.Dtos.Dto.Products;
using AtSepete.Dtos.Dto.Shop;
using AtSepete.Entities.Data;
using AtSepete.UI.ApiResponses.ProductResponse;
using AtSepete.UI.ApiResponses.ShopApiResponse;
using AtSepete.UI.Areas.Admin.Models.ProductVMs;
using AtSepete.UI.Areas.Customer.Models.ShopVMs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NToastNotify;
using SendGrid;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks.Dataflow;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace AtSepete.UI.Areas.Customer.Controllers
{
    public class ShopController : CustomerBaseController
    {
        private readonly IMapper _mapper;

        public ShopController(IToastNotification toastNotification, IConfiguration configuration, IMapper mapper) : base(toastNotification, configuration)
        {
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> HomePage()
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", UserToken);
                using (HttpResponseMessage response = await httpClient.GetAsync($"{ApiBaseUrl}/Shop/ShopList"))
                {
                    if (response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        return RedirectToAction("RefreshTokenLogin", "Login", new { returnUrl = HttpContext.Request.Path, area = "" });
                    }
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    ShopListResponse shopList = JsonConvert.DeserializeObject<ShopListResponse>(apiResponse);

                    if (shopList.IsSuccess)
                    {
                        var products = _mapper.Map<List<ShopListDto>, List<CustomerShopListVM>>(shopList.Data);
                        NotifySuccess(shopList.Message);
                        ViewBag.ShopList = products;
                        //aşağıdaki satırlar diğer action'lardan aldığımız veriler
                        var serializedFilterProducts = TempData["ShopFilterList"] as string;
                        var serializedSideBarFilterProducts = TempData["ShopSideBarFilterList"] as string;
                        if (serializedFilterProducts != null)
                        {
                            var filterProducts = JsonSerializer.Deserialize<List<CustomerShopFilterListVM>>(serializedFilterProducts);
                            ViewBag.FilterProducts = filterProducts;
                        }
                        if (serializedSideBarFilterProducts !=null)
                        {
                            var sideBarFilterProducts = JsonSerializer.Deserialize<List<CustomerShopSideBarFilterListVM>>(serializedSideBarFilterProducts);
                            ViewBag.SideBarFilterProducts = sideBarFilterProducts;
                        }

                    }
                    else
                    {
                        NotifyError(shopList.Message);
                        return RedirectToAction("Index", "Home", new { area = "" });
                    }

                };
                using (HttpResponseMessage responseBestSeller = await httpClient.GetAsync($"{ApiBaseUrl}/Shop/BestSellerProductList"))
                {

                    if (responseBestSeller.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        return RedirectToAction("RefreshTokenLogin", "Login", new { returnUrl = HttpContext.Request.Path, area = "" });
                    }
                    string apiBestSellerResponse = await responseBestSeller.Content.ReadAsStringAsync();
                    BestSellerListResponse bestSellerProductList = JsonConvert.DeserializeObject<BestSellerListResponse>(apiBestSellerResponse);

                    if (bestSellerProductList.IsSuccess)
                    {
                        var bestSellerProducts = _mapper.Map<List<BestSellerProductListDto>, List<CustomerBestSellerListVM>>(bestSellerProductList.Data);
                        NotifySuccess(bestSellerProductList.Message);
                        ViewBag.BestSellerProductList = bestSellerProducts;

                    }
                    else
                    {
                        NotifyError(bestSellerProductList.Message);
                        return RedirectToAction("Index", "Home", new { area = "" });
                    }
                }

                return View();
            }
        }

        public async Task<IActionResult> HomePageSideBarFilter([FromQuery] string sideBarFilter)
        {
            
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", UserToken);
                using (HttpResponseMessage response = await httpClient.GetAsync($"{ApiBaseUrl}/Shop/ShopSideBarFilterList/{sideBarFilter}"))
                {
                    if (response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        return RedirectToAction("RefreshTokenLogin", "Login", new { returnUrl = HttpContext.Request.Path, area = "" });
                    }
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    ShopSideBarFilterListResponse shopSideBarFilterList = JsonConvert.DeserializeObject<ShopSideBarFilterListResponse>(apiResponse);

                    if (shopSideBarFilterList.IsSuccess)
                    {
                        var SideBarFilterproducts = _mapper.Map<List<ShopSideBarFilterListDto>, List<CustomerShopSideBarFilterListVM>>(shopSideBarFilterList.Data);
                        var serializedSideBarFilterProducts = JsonSerializer.Serialize(SideBarFilterproducts);
                        TempData["ShopSideBarFilterList"] = serializedSideBarFilterProducts;
                        NotifySuccess(shopSideBarFilterList.Message);
                        
                        return Json(new { success = shopSideBarFilterList.IsSuccess });
                    }
                    else
                    {
                        NotifyError(shopSideBarFilterList.Message);
                      
                        return Json(new { success = shopSideBarFilterList.IsSuccess });
                    }

                };
                


            }
        }

        public async Task<IActionResult> HomePageFilter(string filterName)
        {
            if (string.IsNullOrEmpty(filterName))
            {
                return RedirectToAction("HomePage");
            }
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", UserToken);
                using (HttpResponseMessage response = await httpClient.GetAsync($"{ApiBaseUrl}/Shop/ShopFilterList/{filterName}"))
                {
                    if (response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        return RedirectToAction("RefreshTokenLogin", "Login", new { returnUrl = HttpContext.Request.Path, area = "" });
                    }
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    ShopFilterListResponse shopFilterList = JsonConvert.DeserializeObject<ShopFilterListResponse>(apiResponse);

                    if (shopFilterList.IsSuccess)
                    {
                        var filterProducts = _mapper.Map<List<ShopFilterListDto>, List<CustomerShopFilterListVM>>(shopFilterList.Data);
                        var serializedFilterProducts = JsonSerializer.Serialize(filterProducts);
                        TempData["ShopFilterList"] = serializedFilterProducts;//homePage de görüntüledik
                        NotifySuccess(shopFilterList.Message);
                        return RedirectToAction("HomePage");
                    }
                    else
                    {
                        NotifyError(shopFilterList.Message);
                        return RedirectToAction("HomePage");
                    }

                };
            }
        }
        public async Task<IActionResult> ShopProductDetails(Guid id)
        {
            if (id==Guid.Empty)
            {
                return RedirectToAction("HomePage");
            }
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", UserToken);
                using (HttpResponseMessage response = await httpClient.GetAsync($"{ApiBaseUrl}/Shop/ShopProductDetails/{id}"))
                {
                    if (response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        return RedirectToAction("RefreshTokenLogin", "Login", new { returnUrl = HttpContext.Request.Path, area = "" });
                    }
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    ShopProductDetailsResponse shopProductDetails = JsonConvert.DeserializeObject<ShopProductDetailsResponse>(apiResponse);

                    if (shopProductDetails.IsSuccess)
                    {
                        var filterProducts = _mapper.Map<List<ShopProductDetailDto>, List<CustomerShopProductDetailsVM>>(shopProductDetails.Data);                       
                        NotifySuccess(shopProductDetails.Message);
                        return View(filterProducts);
                    }
                    else
                    {
                        NotifyError(shopProductDetails.Message);
                        return RedirectToAction("HomePage");
                    }

                };
            }
        }

    }
}
