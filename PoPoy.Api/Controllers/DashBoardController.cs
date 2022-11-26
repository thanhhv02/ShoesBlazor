using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PoPoy.Api.Helpers;
using PoPoy.Api.Services.DashBoard;
using PoPoy.Shared.ViewModels;
using PoPoy.Shared.ViewModels.DashBoard;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PoPoy.Api.Controllers
{
    [AuthorizeToken]
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class DashBoardController : Controller
    {
        private readonly IDashBoardService dashBoardService;

        public DashBoardController(IDashBoardService dashBoardService)
        {
            this.dashBoardService = dashBoardService;
        }

        [HttpGet]
        public async Task<IActionResult> GetReport([FromQuery]  ReportSearchModel input)
        {
            switch (input.ReportType)
            {
                case ReportType.Order:
                    return Ok(await dashBoardService.GetOrderReport(input));
                case ReportType.Income:
                    return Ok(await dashBoardService.GetInComeReport(input));
                case ReportType.Customer:
                    return Ok(await dashBoardService.GetCustomReport(input));
                default:
                    List<ServiceResponse<ReportModel>> list = new();
                    var order = await dashBoardService.GetOrderReport(input);
                    var income = await dashBoardService.GetInComeReport(input);
                    var customer = await dashBoardService.GetCustomReport(input);
                    list.Add(order);
                    list.Add(income);
                    list.Add(customer);
                    return Ok(list);
            }
            
        }
        [HttpGet]
        public async Task<IActionResult> GetNoti()
        {
            return Ok(await dashBoardService.GetNoti());

        }

        [HttpGet]
        public async Task<IActionResult> GetOrderNew()
        {
            return Ok(await dashBoardService.GetOrderNew());

        }


        [HttpGet]
        public async Task<IActionResult> GetChartOrder(ReportDateType reportDateType)
        {
            return Ok(await dashBoardService.GetChartOrder(reportDateType));

        }
        [HttpGet]
        public async Task<IActionResult> ReportProductTop()
        {
            return Ok(await dashBoardService.GetProductTop());

        }

        [HttpGet]
        public async Task<IActionResult> ReportOrderStatus()
        {
            return Ok(await dashBoardService.GetOrderStatus());

        }

    }
}
