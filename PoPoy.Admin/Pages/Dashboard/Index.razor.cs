using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using PoPoy.Admin.Services.DashBoardService;
using PoPoy.Shared.ViewModels.DashBoard;
using PoPoy.Shared.ViewModels.Report;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace PoPoy.Admin.Pages.Dashboard
{
    public partial class Index
    {
        [Inject] private IDashBoardService dashBoardService { get; set; }
        [Inject] private IToastService toastService { get; set; }

        private ReportModel ReportOrder = new ReportModel();
        private ReportModel ReportInCome = new ReportModel();
        private ReportModel ReportCustomer = new ReportModel();
        private List<NotiActivities> notifications = new();
        private List<ReportProductTop> reportProductTops = new();
        private List<ReportOrderNew> orderNews = new();
        private List<ReportOrderStatus> reportOrderStatus = new();
        private ChartModel orderChart = new();
        private ReportSearchModel searchModel = new();

        protected override async Task OnInitializedAsync()
        {

            await LoadALl();
            await LoadNoti();
            await LoadOrder();
            await LoadChartOrder();
            await GetProductTops();
            await GetReportOrderStatus();

        }
        private async Task GetProductTops()
        {
            var result = await dashBoardService.GetProductTop();
            if (result.Success)
            {
                reportProductTops = result.Data;
            }
            else
            {
                toastService.ShowError(result.Message);
            }
        }

        private async Task GetReportOrderStatus()
        {
            var result = await dashBoardService.GetReportOrderStatus();
            if (result.Success)
            {
                reportOrderStatus = result.Data;
            }
            else
            {
                toastService.ShowError(result.Message);
            }
        }
        private async Task LoadChartOrder()
        {
            var result = await dashBoardService.GetChartOrder(ReportDateType.Month);
            if (result.Success)
            {
                orderChart = result.Data;
            }
            else
            {
                toastService.ShowError(result.Message);
            }
        }

        string FormatAsPrice(object value)
        {
            //Console.WriteLine((decimal)(value));
            //return ((decimal)value).ToString("C0", CultureInfo.CreateSpecificCulture("vi-VN"));
            return String.Format(new System.Globalization.CultureInfo("vi-VN"), "{0:C}", decimal.Parse(value.ToString())).Replace("₫", "VNĐ");
        }
        string FormatAsDay(object value)
        {
            if (value != null)
            {
                return "Ngày " + Convert.ToDateTime(value).ToString("dd");
            }

            return string.Empty;
        }
        private async Task LoadNoti()
        {
            var result = await dashBoardService.GetNoti();
            if (result.Success)
            {
                notifications = result.Data;

            }
        }

        private async Task LoadOrder()
        {
            var result = await dashBoardService.GetOrderNew();
            if (result.Success)
            {
                orderNews = result.Data;

            }
        }
        private async Task LoadALl()
        {
            var input = new ReportSearchModel();
            input.ReportDate = ReportDateType.Day;
            input.ReportType = ReportType.Order;
            await LoadDataReport(input);
            input.ReportType = ReportType.Income;
            await LoadDataReport(input);
            input.ReportType = ReportType.Customer;
            await LoadDataReport(input);

        }
        private async Task LoadDataReport(ReportSearchModel input)
        {
            var result = await dashBoardService.GetReport(input);
            if (result.Success)
            {
                switch (result.Data.Type)
                {

                    case ReportType.Order:
                        ReportOrder = result.Data;
                        break;
                    case ReportType.Income:
                        ReportInCome = result.Data;
                        break;
                    case ReportType.Customer:
                        ReportCustomer = result.Data;
                        break;
                }
            }
            else
            {
                toastService.ShowError(result.Message);
            }
        }
    }
}
