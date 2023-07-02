using AtSepete.Dtos.Dto.Markets;
using AtSepete.Dtos.Dto.Reports;
using AtSepete.UI.ApiResponses.MarketApiResponse;
using AtSepete.UI.ApiResponses.ReportApiResponse;
using AtSepete.UI.Areas.Admin.Models.MarketVMs;
using AtSepete.UI.Areas.Admin.Models.ReportVMs;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NToastNotify;
using System.Net;

namespace AtSepete.UI.Areas.Admin.Controllers
{
    public class ReportController : AdminBaseController
    {
        private readonly IMapper _mapper;

        public ReportController(IToastNotification toastNotification, IConfiguration configuration, IMapper mapper) : base(toastNotification, configuration)
        {
            _mapper = mapper;
        }
        public async Task<IActionResult> ReportPage()
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", UserToken);
                using (HttpResponseMessage response = await httpClient.GetAsync($"{ApiBaseUrl}/Report/GetAllReportCount"))
                {
                    if (response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        return RedirectToAction("RefreshTokenLogin", "Login", new { returnUrl = HttpContext.Request.Path, area = "" });
                    }
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    ReportCountResponse reportCountDetail = JsonConvert.DeserializeObject<ReportCountResponse>(apiResponse);
                    if (reportCountDetail.IsSuccess)
                    {
                        var markets = _mapper.Map<ReportCountDto, AdminReportCountVM>(reportCountDetail.Data);
                        NotifySuccessLocalized(reportCountDetail.Message);
                        return View(markets);
                    }
                    else
                    {
                        NotifyErrorLocalized(reportCountDetail.Message);
                        return RedirectToAction("Index", "Admin");
                    }
                };

            }
        }
    }
}
