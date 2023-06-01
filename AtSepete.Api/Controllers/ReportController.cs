using AtSepete.Business.Abstract;
using AtSepete.Dtos.Dto.Categories;
using AtSepete.Dtos.Dto.Reports;
using AtSepete.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AtSepete.Api.Controllers
{
    [Route("AtSepeteApi/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;

        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }
        [HttpGet]
        [Route("[action]")]
        public async Task<IDataResult<ReportCountDto>> GetAllReportCount()
        {
            return await _reportService.GetAllReportCountAsync();
        }

    }

}

