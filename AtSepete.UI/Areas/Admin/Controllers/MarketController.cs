using AtSepete.Dtos.Dto.Categories;
using AtSepete.Dtos.Dto.Markets;
using AtSepete.UI.ApiResponses.CategoryApiResponse;
using AtSepete.UI.ApiResponses.MarketApiResponse;
using AtSepete.UI.Areas.Admin.Models.CategoryVMs;
using AtSepete.UI.Areas.Admin.Models.MarketVMs;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NToastNotify;
using System.Net;
using System.Text;

namespace AtSepete.UI.Areas.Admin.Controllers
{
    public class MarketController : AdminBaseController
    {
        private readonly IMapper _mapper;

        public MarketController(IToastNotification toastNotification, IConfiguration configuration, IMapper mapper) : base(toastNotification, configuration)
        {
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> MarketList()
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", UserToken);
                using (HttpResponseMessage response = await httpClient.GetAsync($"{ApiBaseUrl}/Market/GetAllMarket"))
                {
                    if (response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        return RedirectToAction("RefreshTokenLogin", "Login", new { returnUrl = HttpContext.Request.Path, area = "" });
                    }
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    MarketListResponse marketList = JsonConvert.DeserializeObject<MarketListResponse>(apiResponse);
                    if (marketList.IsSuccess)
                    {
                        var markets = _mapper.Map<List<MarketListDto>, List<AdminMarketListVM>>(marketList.Data);
                        NotifySuccessLocalized(marketList.Message);
                        return View(markets);
                    }
                    else
                    {
                        NotifyErrorLocalized(marketList.Message);
                        return RedirectToAction("Index", "Admin");
                    }
                };

            }
        }
        [HttpGet]
        public async Task<IActionResult> AddMarket()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddMarket(AdminMarketCreateVM adminMarketCreateVM)
        {
            using (var httpClient = new HttpClient())
            {
                CreateMarketDto createMarketDto = _mapper.Map<AdminMarketCreateVM, CreateMarketDto>(adminMarketCreateVM);
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", UserToken);
                StringContent content = new StringContent(JsonConvert.SerializeObject(createMarketDto), Encoding.UTF8, "application/Json");
                using (HttpResponseMessage response = await httpClient.PostAsync($"{ApiBaseUrl}/Market/AddMarket", content))
                {
                    if (response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        return RedirectToAction("RefreshTokenLogin", "Login", new { returnUrl = HttpContext.Request.Path, area = "" });
                    }
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    AddMarketResponse addedMarket = JsonConvert.DeserializeObject<AddMarketResponse>(apiResponse);
                    if (addedMarket.IsSuccess)
                    {
                        NotifySuccessLocalized(addedMarket.Message);
                        return RedirectToAction("MarketList");
                    }
                    else
                    {
                        NotifyErrorLocalized(addedMarket.Message);
                        return View(adminMarketCreateVM);
                    }
                };

            }
        }
        [HttpGet]
        public async Task<IActionResult> DetailMarket(Guid id)
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
                        NotifySuccessLocalized(detailMarket.Message);
                        return View(market);
                    }
                    else
                    {
                        NotifyErrorLocalized(detailMarket.Message);
                        return RedirectToAction("MarketList");
                    }
                };

            }
        }
        [HttpGet]
        public async Task<IActionResult> UpdateMarket(Guid id)
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
                        NotifySuccessLocalized(updateMarket.Message);
                        return View(market);
                    }
                    else
                    {
                        NotifyErrorLocalized(updateMarket.Message);
                        return RedirectToAction("MarketList");
                    }
                };

            }
        }
        [HttpPost]
        public async Task<IActionResult> UpdateMarket(AdminMarketUpdateVM adminMarketUpdateVM)
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
                        NotifySuccessLocalized(updateMarket.Message);
                        return RedirectToAction("MarketList");
                    }
                    else
                    {
                        NotifyErrorLocalized(updateMarket.Message);
                        return View(adminMarketUpdateVM);
                    }
                };

            }
        }
        [HttpGet]
        public async Task<IActionResult> DeleteMarket(Guid id)
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
    }
}
