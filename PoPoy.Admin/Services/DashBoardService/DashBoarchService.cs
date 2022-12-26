using PoPoy.Admin.Extensions;
using PoPoy.Shared.Dto;
using PoPoy.Shared.Entities.OrderDto;
using PoPoy.Shared.ViewModels;
using PoPoy.Shared.ViewModels.DashBoard;
using PoPoy.Shared.ViewModels.Report;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace PoPoy.Admin.Services.DashBoardService
{
    public class DashBoarchService : IDashBoardService
    {

        private readonly HttpClient httpClient;

        public DashBoarchService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<ServiceResponse<ReportModel>> GetReport(ReportSearchModel input)
        {
            var resp = await httpClient.GetFromJsonAsync<ServiceResponse<ReportModel>>($"/api/DashBoard/GetReport?ReportType={input.ReportType}&ReportDate={input.ReportDate}");
            return resp;
        }

        public async Task<ServiceResponse<List<NotiActivities>>> GetNoti()
        {
            var resp = await httpClient.GetFromJsonAsync<ServiceResponse<List<NotiActivities>>>($"/api/DashBoard/GetNoti");
            return resp;
        }
        public async Task<ServiceResponse<List<ReportProductTop>>> GetProductTop()
        {
            var resp = await httpClient.GetFromJsonAsync<ServiceResponse<List<ReportProductTop>>>($"/api/DashBoard/ReportProductTop");
            return resp;
        }

        public async Task<ServiceResponse<List<ReportOrderNew>>> GetOrderNew()
        {
            var resp = await httpClient.GetFromJsonAsync<ServiceResponse<List<ReportOrderNew>>>($"/api/DashBoard/GetOrderNew");
            return resp;
        }
        public async Task<ServiceResponse<ChartModel>> GetChartOrder(ReportDateType type)
        {
            var resp = await httpClient.GetFromJsonAsync<ServiceResponse<ChartModel>>($"/api/DashBoard/GetChartOrder?ReportDateType={type}");
            return resp;
        }



        public async Task<List<ServiceResponse<ReportModel>>> GetReportAll(ReportSearchModel input)
        {
            var resp = await httpClient.GetFromJsonAsync<List<ServiceResponse<ReportModel>>>($"/api/DashBoard/GetReport?ReportDate={input.ReportDate}");
            return resp;
        }

        public async Task<List<Order>> GetOrderByShipper(OrderShipperSearchDto input)
        {
            var result = await httpClient.PostAsync($"/api/order/GetOrderByShipper", input.ToJsonBody());
            //await result.CheckAuthorized(this);
            return await result.Content.ReadFromJsonAsync<List<Order>>();
        }

        public async Task<ServiceResponse<List<ReportOrderStatus>>> GetReportOrderStatus()
        {
            var resp = await httpClient.GetFromJsonAsync<ServiceResponse<List<ReportOrderStatus>>>($"/api/DashBoard/ReportOrderStatus");
            return resp;
        }
    }
}
