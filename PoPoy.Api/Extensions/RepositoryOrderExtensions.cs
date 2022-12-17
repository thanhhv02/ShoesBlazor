using PoPoy.Shared.Dto;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Linq.Dynamic.Core;
using System;
using PoPoy.Shared.Paging;
using PoPoy.Api.Data;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using PoPoy.Shared.ViewModels;

namespace PoPoy.Api.Extensions
{
    public static class RepositoryOrderExtensions
    {
        public static IQueryable<OrderOverviewResponse> Search(this IQueryable<OrderOverviewResponse> Orders, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return Orders;
            var lowerCaseSearchTerm = searchTerm.Trim().ToLower();
            return Orders.Where(p => p.Id.ToLower().Contains(lowerCaseSearchTerm) 
            || p.PaymentMode.ToString().ToLower().Contains(lowerCaseSearchTerm)
            || p.OrderStatus.ToString().ToLower().Contains(lowerCaseSearchTerm)
            || p.TotalPrice.ToString().ToLower().Contains(lowerCaseSearchTerm)
            || p.Product.ToString().ToLower().Contains(lowerCaseSearchTerm));
        }
        public static IQueryable<Order> Sort(this IQueryable<Order> products, string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
                return products.OrderBy(e => e.OrderDate);

            var orderParams = orderByQueryString.Trim().Split(',');
            var propertyInfos = typeof(Order).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var orderQueryBuilder = new StringBuilder();

            foreach (var param in orderParams)
            {
                if (string.IsNullOrWhiteSpace(param))
                    continue;

                var propertyFromQueryName = param.Split(" ")[0];
                var objectProperty = propertyInfos.FirstOrDefault(pi => pi.Name.Equals(propertyFromQueryName, StringComparison.InvariantCultureIgnoreCase));

                if (objectProperty == null)
                    continue;

                var direction = param.EndsWith(" desc") ? "descending" : "ascending";
                orderQueryBuilder.Append($"{objectProperty.Name} {direction}, ");
            }

            var orderQuery = orderQueryBuilder.ToString().TrimEnd(',', ' ');
            if (string.IsNullOrWhiteSpace(orderQuery))
                return products.OrderBy(e => e.OrderDate);

            return products.OrderBy(orderQuery);
        }
       
    }
}
