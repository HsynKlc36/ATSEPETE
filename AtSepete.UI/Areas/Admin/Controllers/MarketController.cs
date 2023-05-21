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

namespace AtSepete.UI.Areas.Admin.Controllers
{
    public class MarketController : AdminBaseController
    {
        private readonly IMapper _mapper;

        public MarketController(IToastNotification toastNotification, IMapper mapper):base(toastNotification)
        {
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> MarketList()
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", UserToken);
                using (HttpResponseMessage response = await httpClient.GetAsync($"https://localhost:7286/AtSepeteApi/Market/GetAllMarket"))
                {
                    if (response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        return RedirectToAction("RefreshTokenLogin", "Login", new { returnUrl = HttpContext.Request.Path });
                    }
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    MarketListResponse marketList = JsonConvert.DeserializeObject<MarketListResponse>(apiResponse);
                    if (marketList.IsSuccess)
                    {
                        var markets = _mapper.Map<List<MarketListDto>, List<AdminMarketListVM>>(marketList.Data);
                        NotifySuccess(marketList.Message);
                        return View(markets);
                    }
                    else
                    {
                        NotifyError(marketList.Message);
                        return RedirectToAction("Index", "Admin");
                    }
                };

            }
        }
    }
}
