#pragma checksum "D:\Myproject\CSharp\DATN2\PoPoy.Client\Pages\CartShow.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "e362c9780f94120fead2af57d43f3bcd7aff3db5"
// <auto-generated/>
#pragma warning disable 1591
namespace PoPoy.Client.Pages
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
#nullable restore
#line 1 "D:\Myproject\CSharp\DATN2\PoPoy.Client\_Imports.razor"
using System.Net.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\Myproject\CSharp\DATN2\PoPoy.Client\_Imports.razor"
using System.Net.Http.Json;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "D:\Myproject\CSharp\DATN2\PoPoy.Client\_Imports.razor"
using Microsoft.AspNetCore.Components.Forms;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "D:\Myproject\CSharp\DATN2\PoPoy.Client\_Imports.razor"
using Microsoft.AspNetCore.Components.Routing;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "D:\Myproject\CSharp\DATN2\PoPoy.Client\_Imports.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "D:\Myproject\CSharp\DATN2\PoPoy.Client\_Imports.razor"
using Microsoft.AspNetCore.Components.Web.Virtualization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "D:\Myproject\CSharp\DATN2\PoPoy.Client\_Imports.razor"
using Microsoft.AspNetCore.Components.WebAssembly.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "D:\Myproject\CSharp\DATN2\PoPoy.Client\_Imports.razor"
using Microsoft.JSInterop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "D:\Myproject\CSharp\DATN2\PoPoy.Client\_Imports.razor"
using Blazored.LocalStorage;

#line default
#line hidden
#nullable disable
#nullable restore
#line 10 "D:\Myproject\CSharp\DATN2\PoPoy.Client\_Imports.razor"
using PoPoy.Client;

#line default
#line hidden
#nullable disable
#nullable restore
#line 11 "D:\Myproject\CSharp\DATN2\PoPoy.Client\_Imports.razor"
using PoPoy.Client.Pages.Ultilities;

#line default
#line hidden
#nullable disable
#nullable restore
#line 12 "D:\Myproject\CSharp\DATN2\PoPoy.Client\_Imports.razor"
using PoPoy.Client.Pages.AccountView;

#line default
#line hidden
#nullable disable
#nullable restore
#line 13 "D:\Myproject\CSharp\DATN2\PoPoy.Client\_Imports.razor"
using PoPoy.Client.Shared;

#line default
#line hidden
#nullable disable
#nullable restore
#line 14 "D:\Myproject\CSharp\DATN2\PoPoy.Client\_Imports.razor"
using PoPoy.Client.Services.AuthService;

#line default
#line hidden
#nullable disable
#nullable restore
#line 15 "D:\Myproject\CSharp\DATN2\PoPoy.Client\_Imports.razor"
using Blazored.Toast;

#line default
#line hidden
#nullable disable
#nullable restore
#line 16 "D:\Myproject\CSharp\DATN2\PoPoy.Client\_Imports.razor"
using Blazored.Toast.Services;

#line default
#line hidden
#nullable disable
#nullable restore
#line 17 "D:\Myproject\CSharp\DATN2\PoPoy.Client\_Imports.razor"
using Microsoft.AspNetCore.Components.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 18 "D:\Myproject\CSharp\DATN2\PoPoy.Client\_Imports.razor"
using PoPoy.Shared.Common;

#line default
#line hidden
#nullable disable
#nullable restore
#line 19 "D:\Myproject\CSharp\DATN2\PoPoy.Client\_Imports.razor"
using PoPoy.Client.Shared.PaginationView;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "D:\Myproject\CSharp\DATN2\PoPoy.Client\Pages\CartShow.razor"
using PoPoy.Client.Services.ProductService;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "D:\Myproject\CSharp\DATN2\PoPoy.Client\Pages\CartShow.razor"
using PoPoy.Client.Services.CartService;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "D:\Myproject\CSharp\DATN2\PoPoy.Client\Pages\CartShow.razor"
using PoPoy.Client.State;

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "D:\Myproject\CSharp\DATN2\PoPoy.Client\Pages\CartShow.razor"
using PoPoy.Shared.Dto;

#line default
#line hidden
#nullable disable
    [Microsoft.AspNetCore.Components.RouteAttribute("/cart")]
    public partial class CartShow : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
            __builder.OpenComponent<PoPoy.Client.Pages.Ultilities.PageInfos>(0);
            __builder.AddAttribute(1, "Title", "Giỏ hàng");
            __builder.CloseComponent();
            __builder.AddMarkupContent(2, "\n\n");
            __builder.OpenComponent<PoPoy.Client.Shared.BackToTop>(3);
            __builder.CloseComponent();
#nullable restore
#line 14 "D:\Myproject\CSharp\DATN2\PoPoy.Client\Pages\CartShow.razor"
 if (carts is null)
{

#line default
#line hidden
#nullable disable
            __builder.AddMarkupContent(4, "<h2>Giỏ hàng</h2>\n                ");
            __builder.OpenComponent<PoPoy.Client.Shared.LoadingScreen>(5);
            __builder.CloseComponent();
#nullable restore
#line 17 "D:\Myproject\CSharp\DATN2\PoPoy.Client\Pages\CartShow.razor"
                                                }
            else if (carts.Count == 0)
            {

#line default
#line hidden
#nullable disable
            __builder.AddMarkupContent(6, "<h2>Hiện tại không có sản phẩm nào trong giỏ hàng của bạn.</h2>");
#nullable restore
#line 20 "D:\Myproject\CSharp\DATN2\PoPoy.Client\Pages\CartShow.razor"
                                                                }
else
{

#line default
#line hidden
#nullable disable
            __builder.AddMarkupContent(7, "<h2>Giỏ hàng</h2>");
            __builder.OpenElement(8, "div");
            __builder.AddAttribute(9, "class", "row");
            __builder.OpenElement(10, "div");
            __builder.AddAttribute(11, "class", "col-sm-9");
            __builder.AddMarkupContent(12, "<hr>");
#nullable restore
#line 28 "D:\Myproject\CSharp\DATN2\PoPoy.Client\Pages\CartShow.razor"
                         foreach (var cart in carts)
                        {

#line default
#line hidden
#nullable disable
            __builder.OpenElement(13, "div");
            __builder.AddAttribute(14, "class", "row");
#nullable restore
#line 31 "D:\Myproject\CSharp\DATN2\PoPoy.Client\Pages\CartShow.razor"
             if (cart.Product.ProductImages != null && cart.Product.ProductImages.Any())
            {

#line default
#line hidden
#nullable disable
            __builder.OpenElement(15, "img");
            __builder.AddAttribute(16, "src", 
#nullable restore
#line 33 "D:\Myproject\CSharp\DATN2\PoPoy.Client\Pages\CartShow.razor"
           cart.Product.ProductImages.FirstOrDefault().ImagePath

#line default
#line hidden
#nullable disable
            );
            __builder.AddAttribute(17, "class", "col-sm-2");
            __builder.AddAttribute(18, "style", " height: 150px; object-fit: contain;");
            __builder.CloseElement();
#nullable restore
#line 33 "D:\Myproject\CSharp\DATN2\PoPoy.Client\Pages\CartShow.razor"
                                                                                                                                }

#line default
#line hidden
#nullable disable
            __builder.OpenElement(19, "div");
            __builder.AddAttribute(20, "class", "col-sm-8");
            __builder.OpenElement(21, "h4");
            __builder.AddContent(22, 
#nullable restore
#line 36 "D:\Myproject\CSharp\DATN2\PoPoy.Client\Pages\CartShow.razor"
                     cart.Product.Title

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.AddMarkupContent(23, "\n                ");
            __builder.OpenComponent<PoPoy.Client.Shared.SelectQuantity>(24);
            __builder.AddAttribute(25, "Model", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<PoPoy.Shared.Dto.Cart>(
#nullable restore
#line 37 "D:\Myproject\CSharp\DATN2\PoPoy.Client\Pages\CartShow.razor"
                                        cart

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(26, "CartUpdateType", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<CartUpdateType>(
#nullable restore
#line 37 "D:\Myproject\CSharp\DATN2\PoPoy.Client\Pages\CartShow.razor"
                                                              CartUpdateType.Update

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(27, "QuantityChanged", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<Microsoft.AspNetCore.Components.EventCallback>(Microsoft.AspNetCore.Components.EventCallback.Factory.Create(this, 
#nullable restore
#line 37 "D:\Myproject\CSharp\DATN2\PoPoy.Client\Pages\CartShow.razor"
                                                                                                      () => StateHasChanged()

#line default
#line hidden
#nullable disable
            )));
            __builder.CloseComponent();
            __builder.AddMarkupContent(28, "\n                ");
            __builder.OpenElement(29, "div");
            __builder.AddAttribute(30, "class", "d-flex flex-row align-items-center mt-3");
            __builder.AddMarkupContent(31, "<i class=\"oi oi-trash\"></i>\n                    ");
            __builder.OpenElement(32, "a");
            __builder.AddAttribute(33, "href", "javascript:void(0)");
            __builder.AddAttribute(34, "onclick", Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.Web.MouseEventArgs>(this, 
#nullable restore
#line 40 "D:\Myproject\CSharp\DATN2\PoPoy.Client\Pages\CartShow.razor"
                                                             () => RemoveCart(cart)

#line default
#line hidden
#nullable disable
            ));
            __builder.AddMarkupContent(35, "Xóa");
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.AddMarkupContent(36, "\n            ");
            __builder.OpenElement(37, "div");
            __builder.AddAttribute(38, "class", "col-sm-2");
            __builder.OpenElement(39, "h4");
            __builder.AddContent(40, 
#nullable restore
#line 44 "D:\Myproject\CSharp\DATN2\PoPoy.Client\Pages\CartShow.razor"
                     cart.Product.Price.ToString("n0")

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.AddMarkupContent(41, "\n                            <hr>");
#nullable restore
#line 47 "D:\Myproject\CSharp\DATN2\PoPoy.Client\Pages\CartShow.razor"
                                  }

#line default
#line hidden
#nullable disable
            __builder.CloseElement();
            __builder.AddMarkupContent(42, "\n                    ");
            __builder.OpenElement(43, "div");
            __builder.AddAttribute(44, "class", "col-sm-3");
            __builder.OpenElement(45, "div");
            __builder.AddAttribute(46, "class", "card");
            __builder.OpenElement(47, "div");
            __builder.AddAttribute(48, "class", "card-body");
            __builder.OpenElement(49, "h5");
            __builder.AddAttribute(50, "class", "card-subtitle mb-2");
            __builder.AddMarkupContent(51, "Tổng số ");
            __builder.AddContent(52, 
#nullable restore
#line 52 "D:\Myproject\CSharp\DATN2\PoPoy.Client\Pages\CartShow.razor"
                                                                         CalcTotalQuantity()

#line default
#line hidden
#nullable disable
            );
            __builder.AddMarkupContent(53, " sản phẩm (đã bao gồm thuế) ");
            __builder.CloseElement();
            __builder.AddMarkupContent(54, "\n                                ");
            __builder.OpenElement(55, "h4");
            __builder.AddAttribute(56, "class", "card-title text-danger");
            __builder.AddContent(57, 
#nullable restore
#line 53 "D:\Myproject\CSharp\DATN2\PoPoy.Client\Pages\CartShow.razor"
                                                                    CalcTotalAmount().ToString("n0")

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.AddMarkupContent(58, "\n                                ");
            __builder.OpenElement(59, "div");
            __builder.AddAttribute(60, "class", "d-grid gap-2 mx-auto");
            __builder.OpenElement(61, "button");
            __builder.AddAttribute(62, "class", "btn btn-warning");
            __builder.AddAttribute(63, "onclick", Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.Web.MouseEventArgs>(this, 
#nullable restore
#line 55 "D:\Myproject\CSharp\DATN2\PoPoy.Client\Pages\CartShow.razor"
                                                                                () => GoToPayment()

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(64, "disabled", 
#nullable restore
#line 55 "D:\Myproject\CSharp\DATN2\PoPoy.Client\Pages\CartShow.razor"
                                                                                                                 buttonDisabled

#line default
#line hidden
#nullable disable
            );
            __builder.AddMarkupContent(65, "Tiếp tục đặt hàng");
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.CloseElement();
#nullable restore
#line 60 "D:\Myproject\CSharp\DATN2\PoPoy.Client\Pages\CartShow.razor"
                      }

#line default
#line hidden
#nullable disable
        }
        #pragma warning restore 1998
#nullable restore
#line 62 "D:\Myproject\CSharp\DATN2\PoPoy.Client\Pages\CartShow.razor"
        private List<Cart> carts;
    private bool buttonDisabled;

    protected override async Task OnInitializedAsync()
    {
        carts = await CartService.GetAllAsync();
        await CartState.UpdateAsync();
    }

    private decimal CalcTotalAmount()
        => carts.Sum(x => x.CalcAmount());

    private int CalcTotalQuantity()
        => carts.Sum(x => x.Quantity);

    private async Task RemoveCart(Cart cart)
    {
        await CartService.RemoveAsync(cart.Product.Id);
        carts = await CartService.GetAllAsync();
        StateHasChanged();
    }

    private async Task GoToPayment()
    {
        try
        {
            buttonDisabled = true;

            //var checkoutUrl = await PaymentService.GetCheckoutUrlAsync(carts);
            //NavigationManager.NavigateTo(checkoutUrl);
        }
        finally
        {
            buttonDisabled = false;
        }
    } 

#line default
#line hidden
#nullable disable
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private ICartState CartState { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private NavigationManager NavigationManager { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private ICartService CartService { get; set; }
    }
}
#pragma warning restore 1591
