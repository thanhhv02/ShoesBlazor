using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;
using PoPoy.Admin.Extensions;
using PoPoy.Admin.Services.AuthService;
using PoPoy.Admin.Services.OrderService;
using PoPoy.Shared.Common;
using PoPoy.Shared.Dto;
using PoPoy.Shared.Dto.Chats;
using PoPoy.Shared.Enum;
using PoPoy.Shared.ViewModels;
using Radzen.Blazor.Rendering;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace PoPoy.Admin.Services.BroadCastService
{
    public class BroadCastService : IBroadCastService
    {
        private readonly HttpClient httpClient;
        private readonly IAuthService authService;
        private readonly ILocalStorageService localStorageService;
        private readonly IOrderService orderService;
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        private readonly IConfiguration configuration;

        public BroadCastService(HttpClient httpClient, IAuthService authService, ILocalStorageService localStorageService, IOrderService orderService, AuthenticationStateProvider authenticationStateProvider, IConfiguration configuration)
        {
            this.httpClient = httpClient;
            this.authService = authService;
            this.localStorageService = localStorageService;
            this.orderService = orderService;
            _authenticationStateProvider = authenticationStateProvider;
            this.configuration = configuration;
        }

        public async Task<ServiceResponse<List<NotificationDto>>> GetNotificationsByUserJwt()
        {

            var cl = await _authenticationStateProvider.GetAuthenticationStateAsync();
            var id = cl.User.GetUserId();
            var result = await httpClient.GetFromJsonAsync<ServiceResponse<List<NotificationDto>>>($"/api/BroadCast/GetAllNotiByUserId?id={id}");


            return result;
        }

        public async Task SendNotiAllAdmin(NotiSendConfig config)
        {
            var data = SetConfigNoti(config);
            var resp = await httpClient.PostAsync($"/api/BroadCast/SendNotiAllAdmin", data.ToJsonBody());
        }


        public async Task SendNotiCurrentUser(NotiSendConfig config) // send cho chính mình 
        {
            var data = SetConfigNoti(config);
            var cl = await _authenticationStateProvider.GetAuthenticationStateAsync();
            var id = cl.User.GetUserId();
            data.UserId = Guid.Parse(id);
            var resp = await httpClient.PostAsync($"/api/BroadCast/SendNotiUserId", data.ToJsonBody());
        }


        public async Task SendNotiUserId(NotiSendConfig config, Guid UserId)
        {
            var data = SetConfigNoti(config);
            if (UserId != Guid.Empty)
            {
                data.UserId = UserId;
                var resp = await httpClient.PostAsync($"/api/BroadCast/SendNotiUserId", data.ToJsonBody());
            }
        }

        public async Task SendMessageAllAdmin(string message, string data = null)
        {

            CreateOrUpdateChatDto model = new() { Data = data, Message = message, Avatar = await GetUserAvtChat() };
            var resp = await httpClient.PostAsync($"/api/BroadCast/SendMessageAllAdmin", model.ToJsonBody());

        }


        public async Task SendMessageUserId(string message, Guid? receiverId, string data = null)
        {

            CreateOrUpdateChatDto model = new() { Data = data, Message = message, ReceiverId = receiverId, Avatar = await GetUserAvtChat() };
            var resp = await httpClient.PostAsync($"/api/BroadCast/SendMessageUserId", model.ToJsonBody());

        }
        public async Task ReadChat(Guid senderId) 
        {
            if (senderId == Guid.Empty)
            {
                return;
            }
            var cl = await _authenticationStateProvider.GetAuthenticationStateAsync();
            var id = cl.User.GetUserId();
         
            var resp = await httpClient.PostAsync($"/api/BroadCast/ReadMessage?receiverId={id}&senderId={senderId}" , null);
        }

        public async Task<ServiceResponse<List<ListChatSender>>> GetListChatSender()
        {

            var cl = await _authenticationStateProvider.GetAuthenticationStateAsync();
            var id = cl.User.GetUserId();
            var result = await httpClient.GetFromJsonAsync<ServiceResponse<List<ListChatSender>>>($"/api/BroadCast/GetListChatSender?userId={id}");


            return result;
        }

        public async Task<string> GetUserIdCurrentChat()
        {
            var cl = await _authenticationStateProvider.GetAuthenticationStateAsync();
            return cl.User.GetUserId();
        }

        public async Task<string> GetUserAvtChat()
        {
            string AvatarPath = string.Empty;

            var temp = await authService.GetUserFromId(await GetUserIdCurrentChat());
            AvatarPath = temp.Data.AvatarPath;
            return AvatarPath;
        }



        public async Task<ServiceResponse<List<ListChatUser>>> GetListChatUser()
        {

            var cl = await _authenticationStateProvider.GetAuthenticationStateAsync();
            var id = cl.User.GetUserId();
            var result = await httpClient.GetFromJsonAsync<ServiceResponse<List<ListChatUser>>>($"/api/BroadCast/GetListChatUser?userId={id}");


            return result;
        }


        private CreateOrUpdateNotiDto SetConfigNoti(NotiSendConfig config)
        {
            CreateOrUpdateNotiDto data = new CreateOrUpdateNotiDto()
            {
                Data = config.Data,
                DataUrl = config.DataUrl,
                Title = config.Title,
                Message = config.Message,
            };
            return data;

        }

        public async Task SendInfoOrderId(string orderId)
        {
            //await orderService.GetOrderDetails(orderId);
            //var order = orderService.ListOrderDetailsResponse;
            //var products = order.Products;
            //var parent = @"
            //        <div class=""d-flex justify-content-center row"">
            //        <div class=""col-md-6"">";
            //foreach (var item in products)
            //{
            //    var image = item.ProductImages.Count > 0 ? item.ProductImages[0] : null;
            //    var p = @$"<div class=""row p-2 bg-white border rounded"">
            //                <div class=""col-md-3 mt-1""><img class=""img-fluid img-responsive rounded product-image"" src=""{image}""></div>
            //                <div class=""col-md-6 mt-1"">
            //                    <h5>{item.Title}</h5>
            //                    <p class=""text-justify text-truncate para mb-0"">Ngày đặt: {order.OrderDate}<br><br></p>
            //                    <p class=""text-justify text-truncate para mb-0"">Trạng thái: {order.OrderStatus}<br><br></p>
            //                    <p class=""text-justify text-truncate para mb-0"">Tổng tiền: {order.TotalPrice}<br><br></p>
            //                    <p class=""text-justify text-truncate para mb-0"">Phương thức: {order.PaymentMode}<br><br></p>
            //                </div>
            //                <div class=""align-items-center align-content-center col-md-3 border-left mt-1"">
            //                    <div class=""d-flex flex-column mt-4""><a href=""/listOrderDetails/{orderId}"" class=""btn btn-primary btn-sm"" >Xem đơn hàng</a></div>
            //                </div>
            //            </div>";

            //    parent += p;
            //}
            var parent = @"</div></div> """;
            await SendMessageAllAdmin("{{html}}", parent);
        }


        public async Task SendInfoOrderProductModel(OrderDetailsProductResponse product, OrderDetailsResponse order)
        {
            var image = product.ProductImages.Count > 0 ? product.ProductImages[0].ImagePath : null;

            var parent = @$"
                    <div class=""row"">
                    
                    <div class=""col-12"">
                        <div class=""row"">
                            <img class=""col-4"" style=""max-height:100px;"" src=""{image}"" />

                             <p class=""col-8""><b>{product.Title}</b></p>
                        </div>
                        <div class=""row"">
                            <div class=""col-6""><div class=""h5 pb-2 border-bottom border-danger"">Ngày đặt</div> {order.OrderDate}</div>
                            <div class=""col-6""><div class=""h5 pb-2 border-bottom border-danger"">Trạng thái</div> {order.OrderStatus}</div>
                        </div>
                        <div class=""row"">
                            <div class=""col-6""><div class=""h5 pb-2 border-bottom border-danger"">Tổng tiền</div> {order.TotalPrice}</div>
                            <div class=""col-6""><div class=""h5 pb-2 border-bottom border-danger"">Phương thức</div> {order.PaymentMode}</div>

                        </div>
                        <div>
                            <a href=""order/{product.OrderId}"" class=""btn btn-success col-12 btn-sm"">Xem đơn hàng</a>
                        </div>

                    </div>
                </div>";
            await SendMessageAllAdmin("{{html}}", parent);
        }

        public async Task SendInfoOrderModelToUserId(Order order , Guid userId)
        {

            var parent = @$"
                    <div class=""row"">
                    
                    <div class=""col-12"">
                        <div class=""row"">
                             <p class=""col-12""><b>Thông tin đơn hàng: mã đơn hàng #{order.Id}</b></p>
                        </div>
                        <div class=""row"">
                            <div class=""col-6 text-nowrap""><div class=""h5 pb-2 border-bottom border-danger"">Ngày đặt</div> {order.OrderDate}</div>
                            <div class=""col-6 text-nowrap""><div class=""h5 pb-2 border-bottom border-danger"">Trạng thái</div> {order.OrderStatus}</div>
                        </div>
                        <div class=""row"">
                            <div class=""col-6 text-nowrap""><div class=""h5 pb-2 border-bottom border-danger"">Tổng tiền</div> {order.TotalPrice}</div>
                            <div class=""col-6 text-nowrap""><div class=""h5 pb-2 border-bottom border-danger"">Phương thức</div> {order.PaymentMode}</div>

                        </div>
                        <div>
                            <a href=""order/{order.Id}"" class=""btn btn-success col-12 btn-sm"">Xem đơn hàng</a>
                        </div>

                    </div>
                </div>";
            await SendMessageUserId("{{html}}", userId, parent);
        }


        public async Task ReadNoti(Guid notiId)
        {

            var resp = await httpClient.PostAsync($"/api/BroadCast/ReadNoti?notiId={notiId}", null);
        }

        public async Task ReadAllNoti()
        {
            var cl = await _authenticationStateProvider.GetAuthenticationStateAsync();
            var id = cl.User.GetUserId();
            var resp = await httpClient.PostAsync($"/api/BroadCast/ReadAllNoti?userId={id}", null);
        }

        public async Task<HubConnection> BuidHubWithToken(string broadCastType = BroadCastType.Notify)
        {
            var hubstring = broadCastType == BroadCastType.Notify ? "/notificationHub" : "/chathub";
            var token = await localStorageService.GetItemAsStringAsync("authToken");
            token = token.Remove(0, 1);
            token = token.Remove(token.Length - 1, 1);
            var hostApi = configuration["BackendApiUrl"];
            return new HubConnectionBuilder().WithUrl(hostApi + hubstring, options =>
            {
                options.AccessTokenProvider = () => Task.FromResult(token);
            }).WithAutomaticReconnect().Build();
        }

        public async Task StartAsync(HubConnection hubConnection)
        {
            await hubConnection.StartAsync().ContinueWith(t => {
                if (t.IsFaulted)
                    Console.WriteLine(t.Exception.GetBaseException());
                else
                    Console.WriteLine("Connected to Hub ^^");
            });
        }
    }
}
