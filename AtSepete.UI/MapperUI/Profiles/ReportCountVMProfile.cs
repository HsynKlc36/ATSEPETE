using AtSepete.Dtos.Dto.Reports;
using AtSepete.UI.Areas.Admin.Models.ReportVMs;

namespace AtSepete.UI.MapperUI.Profiles
{
    public class ReportCountVMProfile:Profile
    {
        public ReportCountVMProfile()
        {
            CreateMap<ReportCountDto, AdminReportCountVM>().ReverseMap();
        }
    }
}
