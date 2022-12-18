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

namespace PoPoy.Api.Extensions
{
    public static class RepositoryProductExtensions
    {
        public static IQueryable<Product> Search(this IQueryable<Product> products, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return products;
            var lowerCaseSearchTerm = searchTerm.Trim().ToLower();
            return products.Where(p => p.Title.ToLower().Contains(lowerCaseSearchTerm) ||
                                    p.Description.ToLower().Contains(lowerCaseSearchTerm));
        }
        public static IQueryable<Product> Sort(this IQueryable<Product> products, string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
                return products.OrderBy(e => e.Title);

            //var t = from p in products
            //        join pq in dataContext.ProductQuantities on p.Id equals pq.ProductId
            //        join pc in dataContext.ProductColors on pq.ColorId equals pc.Id
            //        select new
            //        {
            //            p, pq, pc
            //        };
            var orderParams = orderByQueryString.Trim().Split(',');
            var propertyInfos = typeof(Product).GetProperties(BindingFlags.Public | BindingFlags.Instance);
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
                return products.OrderBy(e => e.Title);

            return products.OrderBy(orderQuery);
        }
        public static IQueryable<Product> SortByPrice(this IQueryable<Product> products, string orderByQueryString, DataContext dataContext)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
                return products.OrderBy(e => e.Title);
            if (orderByQueryString.ToLower().Contains("price"))
            {
                if (orderByQueryString.ToLower().EndsWith(" desc"))
                {
                    return (from t1 in products
                            join t2 in dataContext.ProductQuantities on t1.Id equals t2.Id
                            orderby t2.Price descending
                            select t1);
                }
                else
                {
                    return (from t1 in products
                            join t2 in dataContext.ProductQuantities on t1.Id equals t2.Id
                            orderby t2.Price ascending
                            select t1);
                }
            }
            return products;
        }
        public static IQueryable<Product> SortByColor(this IQueryable<Product> products, string orderByQueryString, DataContext dataContext)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
                return products.OrderBy(e => e.Title);
            if (orderByQueryString.ToLower().Contains("price"))
            {

                    return (from t1 in products
                            join t2 in dataContext.ProductQuantities on t1.Id equals t2.Id
                            join t3 in dataContext.ProductColors on t2.ColorId equals t3.Id
                            where t3.Id == int.Parse(orderByQueryString)
                            select t1);

            }
            return products;
        }
    }
}
