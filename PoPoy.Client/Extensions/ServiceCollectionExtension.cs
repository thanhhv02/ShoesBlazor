
using Blazored.LocalStorage;
using Blazored.Toast;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PoPoy.Client.Services;
using PoPoy.Client.Services.AuthService;
using PoPoy.Client.Services.BroadCastService;
using PoPoy.Client.Services.CartService;
using PoPoy.Client.Services.CategoryService;
using PoPoy.Client.Services.HttpRepository;
using PoPoy.Client.Services.OrderService;
using PoPoy.Client.Services.ProductService;
using PoPoy.Client.Services.UserAvatarService;
using PoPoy.Client.State;
using PoPoy.Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PoPoy.Client.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static object Configuration { get; private set; }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<AuthenticationStateProvider, ApiAuthenticationStateProvider>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICartService, CartService>();
            services.AddScoped<ICartState, CartState>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IUserAvatarService, UserAvatarService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IBroadCastService, BroadCastService>();
            services.AddScoped<IReviewService, ReviewService>();
            services.AddScoped<IPublicReviewService, PublicReviewService>();
            services.AddScoped<RefreshTokenService>();
            services.AddScoped<HttpInterceptorService>();
            return services;
        }

        public static IServiceCollection AddNugetPackageServices(this IServiceCollection services)
        {
            services.AddBlazoredLocalStorage();
            services.AddAuthorizationCore();
            services.AddBlazoredToast();
            return services;
        }
    }
}
