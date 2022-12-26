using PoPoy.Shared.ViewModels;
using PoPoy.Shared.ViewModels.DashBoard;
using PoPoy.Shared.ViewModels.Report;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PoPoy.Admin.Services.DashBoardService
{
    public interface IDashBoardService
    {
        Task<ServiceResponse<ReportModel>> GetReport(ReportSearchModel input);
        Task<List<ServiceResponse<ReportModel>>> GetReportAll(ReportSearchModel input);
        Task<ServiceResponse<List<NotiActivities>>> GetNoti();
        Task<ServiceResponse<List<ReportOrderNew>>> GetOrderNew();
        Task<ServiceResponse<ChartModel>> GetChartOrder(ReportDateType type);
        Task<ServiceResponse<List<ReportOrderStatus>>> GetReportOrderStatus();
        Task<ServiceResponse<List<ReportProductTop>>> GetProductTop();
    }
}
