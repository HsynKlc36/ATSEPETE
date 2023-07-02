using AtSepete.Dtos.Dto.ProductMarkets;
using AtSepete.Dtos.Dto.Stocks;
using AtSepete.UI.ApiResponses.ProductMarketApiResponse;
using AtSepete.UI.ApiResponses.StockApiResponse;
using AtSepete.UI.Areas.Admin.Models.ProductMarketVMs;
using AtSepete.UI.Areas.Admin.Models.StockVMs;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NToastNotify;
using System.Net;
using System.Text;

namespace AtSepete.UI.Areas.Admin.Controllers
{
    public class StockController : AdminBaseController
    {
        private readonly IMapper _mapper;
        public StockController(IToastNotification toastNotification, IConfiguration configuration, IMapper mapper) : base(toastNotification, configuration)
        {
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> StockList()
        {
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
                    //ProductMarketListResponse stockList = JsonConvert.DeserializeObject<ProductMarketListResponse>(apiResponse);
                    StockListResponse stockList = JsonConvert.DeserializeObject<StockListResponse>(apiResponse);
                    if (stockList.IsSuccess)
                    {
                        var ProductMarkets = _mapper.Map<List<StockListDto>, List<AdminStockListVM>>(stockList.Data);
                        NotifySuccessLocalized(stockList.Message);
                        return View(ProductMarkets);
                    }
                    else
                    {
                        NotifyErrorLocalized(stockList.Message);
                        return RedirectToAction("Index", "Admin");
                    }
                };

            }
        }

        [HttpGet]
        public async Task<IActionResult> UpdateStock(Guid id)
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
                    DetailStockResponse updateStock = JsonConvert.DeserializeObject<DetailStockResponse>(apiResponse);
                    if (updateStock.IsSuccess)
                    {
                        var productMarket = _mapper.Map<StockDto, AdminUpdateStockVM>(updateStock.Data);
                        NotifySuccess(updateStock.Message);
                        return View(productMarket);
                    }
                    else
                    {
                        NotifyError(updateStock.Message);
                        return RedirectToAction("StockList");
                    }
                };
            }
        }
        [HttpPost]
        public async Task<IActionResult> UpdateStock(AdminUpdateStockVM adminUpdateStockVM)
        {
            using (var httpClient = new HttpClient())
            {
                var updateStockDto = _mapper.Map<AdminUpdateStockVM, UpdateStockDto>(adminUpdateStockVM);

                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", UserToken);
                StringContent content = new StringContent(JsonConvert.SerializeObject(updateStockDto), Encoding.UTF8, "application/Json");
                using (HttpResponseMessage response = await httpClient.PutAsync($"{ApiBaseUrl}/ProductMarket/UpdateProductMarket/{adminUpdateStockVM.Id}", content))
                {
                    if (response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        return RedirectToAction("RefreshTokenLogin", "Login", new { returnUrl = HttpContext.Request.Path, area = "" });
                    }
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    UpdateStockResponse updateStock = JsonConvert.DeserializeObject<UpdateStockResponse>(apiResponse);
                    if (updateStock.IsSuccess)
                    {
                        NotifySuccess(updateStock.Message);
                        return RedirectToAction("StockList");
                    }
                    else
                    {
                        NotifyError(updateStock.Message);
                        return View(adminUpdateStockVM);
                    }
                };

            }
        }
    }
}
