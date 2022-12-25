using MailKit.Search;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Polly;
using PoPoy.Api.Data;
using PoPoy.Shared.Dto;
using PoPoy.Shared.Enum;
using PoPoy.Shared.ViewModels;
using PoPoy.Shared.ViewModels.DashBoard;
using PoPoy.Shared.ViewModels.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace PoPoy.Api.Services.DashBoard
{
    public class DashBoardService  : IDashBoardService
    {
        private readonly DataContext dataContext;
        private readonly UserManager<User> userManager;

        public DashBoardService(DataContext dataContext, UserManager<User> userManager)
        {
            this.dataContext = dataContext;
            this.userManager = userManager;
        }

        public async Task<ServiceResponse<ReportModel>> GetOrderReport(ReportSearchModel reportSearch)
        {
            try
            {
                var query = dataContext.Orders.Where(p => p.OrderStatus == OrderStatus.Delivered);
                var query2 = query.AsEnumerable();
                var datepast = GetDateTime(reportSearch.ReportDate);
                var now = DateTime.UtcNow;
                switch (reportSearch.ReportDate)
                {
                    case ReportDateType.Day:
                        query = query.Where(p => p.OrderDate.Date == now.Date);
                        query2 = query2.Where(p => p.OrderDate.Date == datepast.Date);
                        break;
                    case ReportDateType.Month:
                        query = query.Where(p => p.OrderDate.Month == now.Month && p.OrderDate.Year == now.Year);
                        query2 = query2.Where(p => p.OrderDate.Month == datepast.Month && p.OrderDate.Year == datepast.Year);
                        break;
                    case ReportDateType.Year:
                        query = query.Where(p => p.OrderDate.Year == now.Year);
                        query2 = query2.Where(p => p.OrderDate.Month == datepast.Year);

                        break;
                    default:
                        query = query.Where(p => p.OrderDate.Date == now.Date);
                        query2 = query2.Where(p => p.OrderDate.Date == datepast.Date);
                        break;
                }

                var orders = await query.CountAsync();
                var past = query2.Count();
                var ordersCount = double.Parse(orders.ToString());
                var pastCount = double.Parse(past.ToString());
                var result = new ReportModel();
                string per = "";
                if (ordersCount < pastCount)
                {
                    per = ((pastCount - ordersCount) / pastCount).ToString("0.0%");
                    result.IsDecrease = true;
                }
                else if (ordersCount == 0 && pastCount == 0)
                {
                    per = "0%";
                }
                else { 
                    per = ((ordersCount - pastCount) / ordersCount).ToString("0.0%");
                    result.IsDecrease = false;
                }
                result.Count = ordersCount.ToString();
                result.Percent = per;
                result.Type = ReportType.Order;
                result.DateType = reportSearch.ReportDate;
                return new ServiceSuccessResponse<ReportModel>(result);

            }
            catch (Exception e)
            {

                return new ServiceErrorResponse<ReportModel>(e.Message);

            }

        }

        public async Task<ServiceResponse<ReportModel>> GetInComeReport(ReportSearchModel reportSearch)
        {
            try
            {
                var query = dataContext.Orders.Where(p => p.OrderStatus == OrderStatus.Delivered);

                var query2 = query.AsEnumerable();
                var datepast = GetDateTime(reportSearch.ReportDate);
                var now = DateTime.UtcNow;
                switch (reportSearch.ReportDate)
                {
                    case ReportDateType.Day:
                        query = query.Where(p => p.OrderDate.Date == now.Date);
                        query2 = query2.Where(p => p.OrderDate.Date == datepast.Date);
                        break;
                    case ReportDateType.Month:
                        query = query.Where(p => p.OrderDate.Month == now.Month && p.OrderDate.Year == now.Year);
                        query2 = query2.Where(p => p.OrderDate.Month == datepast.Month && p.OrderDate.Year == datepast.Year);
                        break;
                    case ReportDateType.Year:
                        query = query.Where(p => p.OrderDate.Year == now.Year);
                        query2 = query2.Where(p => p.OrderDate.Year == datepast.Year);

                        break;
                    default:
                        query = query.Where(p => p.OrderDate.Date == now.Date);
                        query2 = query2.Where(p => p.OrderDate.Date == datepast.Date);
                        break;
                }

                var orders = await query.SumAsync(p => p.TotalPrice);
                var past = query2.Sum( p => p.TotalPrice);
                var ordersCount = double.Parse(orders.ToString());
                var pastCount = double.Parse(past.ToString());
                var result = new ReportModel();
                string per = "";
                if (ordersCount < pastCount)
                {
                    per = ((pastCount - ordersCount) / pastCount).ToString("0.0%");
                    result.IsDecrease = true;
                }
                else if (ordersCount == 0 && pastCount == 0)
                {
                    per = "0%";
                }
                else
                {
                    per = ((ordersCount - pastCount) / ordersCount).ToString("0.0%");
                    result.IsDecrease = false;
                }
                result.Count = ordersCount.ToString();
                result.Percent = per;
                result.Type = ReportType.Income;
                result.DateType = reportSearch.ReportDate;
                return new ServiceSuccessResponse<ReportModel>(result);

            }
            catch (Exception e)
            {

                return new ServiceErrorResponse<ReportModel>(e.Message);

            }

        }

        public async Task<ServiceResponse<ReportModel>> GetCustomReport(ReportSearchModel reportSearch)
        {
            try
            {
                var users = await userManager.GetUsersInRoleAsync(RoleName.Customer);
                var query = users.AsQueryable();
                var query2 = users.AsEnumerable();
                var datepast = GetDateTime(reportSearch.ReportDate);
                var now = DateTime.UtcNow;
                switch (reportSearch.ReportDate)
                {
                    case ReportDateType.Day:
                        query = query.Where(p => p.CreatedDate.Date == now.Date);
                        query2 = query2.Where(p => p.CreatedDate.Date == datepast.Date);
                        break;
                    case ReportDateType.Month:
                        query = query.Where(p => p.CreatedDate.Month == now.Month && p.CreatedDate.Year == now.Year);
                        query2 = query2.Where(p => p.CreatedDate.Month == datepast.Month && p.CreatedDate.Year == datepast.Year);
                        break;
                    case ReportDateType.Year:
                        query = query.Where(p => p.CreatedDate.Year == now.Year);
                        query2 = query2.Where(p => p.CreatedDate.Year == datepast.Year);

                        break;
                    default:
                        query = query.Where(p => p.CreatedDate.Date == now.Date);
                        query2 = query2.Where(p => p.CreatedDate.Date == datepast.Date);
                        break;
                }

                var orders =  query.Count();
                var past = query2.Count();

                var ordersCount = double.Parse(orders.ToString());
                var pastCount = double.Parse(past.ToString());
                var result = new ReportModel();
                string per = "";
                if (ordersCount < pastCount)
                {
                    per = ((pastCount - ordersCount) / pastCount).ToString("0.0%");
                    result.IsDecrease = true;
                }
                else if (ordersCount == 0 && pastCount ==0)
                {
                    per = "0%";
                }
                else
                {
                    per = ((ordersCount - pastCount) / ordersCount).ToString("0.0%");
                    result.IsDecrease = false;
                }
                result.Count = ordersCount.ToString();
                result.Percent = per;
                result.Type = ReportType.Customer;
                result.DateType = reportSearch.ReportDate;
                return new ServiceSuccessResponse<ReportModel>(result);

            }
            catch (Exception e)
            {

                return new ServiceErrorResponse<ReportModel>(e.Message);

            }

        }

        public async Task<ServiceResponse<List<NotiActivities>>> GetNoti()
        {
            try
            {
                var list = await dataContext.Notifications.Include(p => p.User)
              .OrderByDescending(p => p.Created).Take(20).ToListAsync();
                var result = new List<NotiActivities>();
                foreach (var item in list)
                {
                    var model = new NotiActivities()
                    {
                        CreatedDate = item.Created,
                        FullName = item.User.FirstName + " " + item.User.LastName,
                        Message = item.Message,
                        IsCustommer = await userManager.IsInRoleAsync(item.User, RoleName.Customer)
                    };
                    result.Add(model);
                }

                return new ServiceSuccessResponse<List<NotiActivities>>(result);
            }
            catch (Exception e)
            {
                return new ServiceErrorResponse<List<NotiActivities>>(e.Message);
            }
        }

        public async Task<ServiceResponse<List<ReportOrderNew>>> GetOrderNew()
        {
            try
            {
                var orderDetails = await dataContext.OrderDetails.Include(p => p.Order).ThenInclude(p => p.User).Include(p => p.Product).OrderByDescending(p => p.Order.OrderDate).Select(p => new ReportOrderNew
                {
                    FullName = p.Order.User.FirstName + " " + p.Order.User.LastName,
                    Price = p.Price,
                    ProductName = p.Product.Title,
                    ProductId = p.ProductId,
                    OrderId = p.Order.Id,
                    Status = p.Order.OrderStatus,
                    Date = p.Order.OrderDate

                }).Take(10).ToListAsync();

                return new ServiceSuccessResponse<List<ReportOrderNew>>(orderDetails);
            }
            catch (Exception e)
            {

               return  new ServiceErrorResponse<List<ReportOrderNew>>(e.Message);
            }

        }

        public async Task<ServiceResponse<List<ReportProductTop>>> GetProductTop()
        {
            try
            {

                var orderDetails = await dataContext.OrderDetails.Include(p => p.Product).ThenInclude(p => p.ProductImages).Where(p => p.Order.OrderStatus == OrderStatus.Delivered).ToListAsync();
                var list = orderDetails.GroupBy(p => p.ProductId);
                var result = new List<ReportProductTop>();
                foreach (var item in list)
                {
                    var p = item.FirstOrDefault();
                    var count = item.Sum(p => p.Quantity);
                    var imageP = p.Product.ProductImages.FirstOrDefault();
                    var image = imageP == null ? null: imageP.ImagePath; 
                    if (p != null)
                    {
                        var itemP = new ReportProductTop { Count = count, Image = image, ProductName = p.Product?.Title, Price = p.Price };
                        result.Add(itemP);
                    }
                }
                result = result.Take(5).OrderByDescending(p => p.Count).ToList();
                return new ServiceSuccessResponse<List<ReportProductTop>>(result);
            }
            catch (Exception e)                     
            {



                return new ServiceErrorResponse<List<ReportProductTop>>(e.Message);
            }

        }

        public async Task<ServiceResponse<List<ReportOrderStatus>>> GetOrderStatus()
        {
            //    Processing,
            //Confirmed,
            //Delivering,
            //Delivered,
            //Cancelled,
            var Processing = await dataContext.Orders.CountAsync(p => p.OrderStatus == OrderStatus.Processing);
            var Confirmed = await dataContext.Orders.CountAsync(p => p.OrderStatus == OrderStatus.Confirmed);
            var Delivering = await dataContext.Orders.CountAsync(p => p.OrderStatus == OrderStatus.Delivering);
            var Delivered = await dataContext.Orders.CountAsync(p => p.OrderStatus == OrderStatus.Delivered);
            var Cancelled = await dataContext.Orders.CountAsync(p => p.OrderStatus == OrderStatus.Cancelled);

            var list = new List<ReportOrderStatus>();
            list.Add(new ReportOrderStatus { Name = "Đang chờ xác nhận", Count = Processing });
            list.Add(new ReportOrderStatus { Name = "Đã xác nhận", Count = Confirmed });
            list.Add(new ReportOrderStatus { Name = "Đang vận chuyển", Count = Delivering });
            list.Add(new ReportOrderStatus { Name = "Thành công", Count = Delivered });
            list.Add(new ReportOrderStatus { Name = "Thất bại", Count = Cancelled });

            return new ServiceSuccessResponse<List<ReportOrderStatus>>(list);
        }

        public async Task<ServiceResponse<ChartModel>> GetChartOrder(ReportDateType reportDateType) 
        {
            try
            {
                var orderDetails = new ChartModel();
                var query = dataContext.Orders.Include(p => p.OrderDetails).Where(p => p.OrderStatus == OrderStatus.Delivered);
                var query2 = query.AsQueryable();
                var datepast = GetDateTime(reportDateType);
                var now = DateTime.UtcNow;
                switch (reportDateType)
                {
                    case ReportDateType.Day:
                        query = query.Where(p => p.OrderDate.Date == now.Date);
                        query2 = query2.Where(p => p.OrderDate.Date == datepast.Date);


                        break;
                    case ReportDateType.Month:
                        query = query.Where(p => p.OrderDate.Month == now.Month && p.OrderDate.Year == now.Year);
                        query2 = query2.Where(p => p.OrderDate.Month == datepast.Month && p.OrderDate.Year == datepast.Year);
                        break;
                    case ReportDateType.Year:
                        query = query.Where(p => p.OrderDate.Year == now.Year);
                        query2 = query2.Where(p => p.OrderDate.Year == datepast.Year);

                        break;
                    default:
                        query = query.Where(p => p.OrderDate.Date == now.Date);
                        query2 = query2.Where(p => p.OrderDate.Date == datepast.Date);
                        break;
                }
                orderDetails.ChartNew = await query.Select(p => new ChartModelItem { Date = p.OrderDate, Count = p.TotalPrice }).ToListAsync();
                orderDetails.ChartOld = await query2.Select(p => new ChartModelItem { Date = p.OrderDate, Count = p.TotalPrice }).ToListAsync();
                return new ServiceSuccessResponse<ChartModel>(orderDetails);
            }
            catch (Exception e)
            {

                return new ServiceErrorResponse<ChartModel>(e.Message);
            }

        }
        private DateTime GetDateTime(ReportDateType reportDateType)
        {
            switch (reportDateType)
            {
                case ReportDateType.Day:
                   return DateTime.UtcNow.AddDays(-1);
                case ReportDateType.Month:
                   return DateTime.UtcNow.AddMonths(-1);
                case ReportDateType.Year:
                    return DateTime.UtcNow.AddYears(-1);
                default: return DateTime.UtcNow.AddDays(-1); 
            }
        }
    }
}
