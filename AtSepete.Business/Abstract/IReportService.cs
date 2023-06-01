using AtSepete.Dtos.Dto.Products;
using AtSepete.Dtos.Dto.Reports;
using AtSepete.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtSepete.Business.Abstract
{
    public interface IReportService
    {
        Task<IDataResult<ReportCountDto>> GetAllReportCountAsync();


    }
}
