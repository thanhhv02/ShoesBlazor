using PoPoy.Shared.ViewModels.DashBoard;
using PoPoy.Shared.ViewModels;
using System.Threading.Tasks;
using PoPoy.Shared.Dto;
using System.Collections.Generic;
using PoPoy.Shared.ViewModels.Report;

namespace PoPoy.Api.Services.DashBoard
{
    public interface IDashBoardService
    {
        Task<ServiceResponse<ReportModel>> GetOrderReport(ReportSearchModel reportSearch);
        Task<ServiceResponse<ReportModel>> GetInComeReport(ReportSearchModel reportSearch);
        Task<ServiceResponse<ReportModel>> GetCustomReport(ReportSearchModel reportSearch);

        Task<ServiceResponse<List<NotiActivities>>> GetNoti();
        Task<ServiceResponse<List<ReportOrderNew>>> GetOrderNew();
        Task<ServiceResponse<ChartModel>> GetChartOrder(ReportDateType reportDateType);
        Task<ServiceResponse<List<ReportProductTop>>> GetProductTop();
        Task<ServiceResponse<List<ReportOrderStatus>>> GetOrderStatus();
    }
}
