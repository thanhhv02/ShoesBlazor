#pragma checksum "D:\Myproject\CSharp\DATN2\PoPoy.Client\Pages\AccountView\ResetPassword.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "a04e67d67387fe934f1a19473211e320d8e42915"
// <auto-generated/>
#pragma warning disable 1591
namespace PoPoy.Client.Pages.AccountView
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
#line 4 "D:\Myproject\CSharp\DATN2\PoPoy.Client\Pages\AccountView\ResetPassword.razor"
using PoPoy.Shared.ViewModels;

#line default
#line hidden
#nullable disable
    [Microsoft.AspNetCore.Components.RouteAttribute("/resetpassword")]
    public partial class ResetPassword : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
            __builder.OpenComponent<PoPoy.Client.Pages.Ultilities.PageInfos>(0);
            __builder.AddAttribute(1, "Title", "Đổi mật khẩu mới");
            __builder.CloseComponent();
#nullable restore
#line 8 "D:\Myproject\CSharp\DATN2\PoPoy.Client\Pages\AccountView\ResetPassword.razor"
 if (!IsClickLink)
{

#line default
#line hidden
#nullable disable
            __builder.OpenElement(2, "div");
            __builder.AddAttribute(3, "class", " text-center ");
            __builder.AddMarkupContent(4, "<br>\r\n    ");
            __builder.AddMarkupContent(5, "<h3>Đổi mật khẩu mới</h3>\r\n    <br>\r\n    ");
            __builder.AddMarkupContent(6, "<div>\r\n        Mail chứa link đổi mật khẩu mới đã được gửi, vui lòng kiểm tra email của bạn.\r\n    </div>\r\n    ");
            __builder.OpenComponent<PoPoy.Client.Shared.BackToTop>(7);
            __builder.CloseComponent();
            __builder.CloseElement();
#nullable restore
#line 19 "D:\Myproject\CSharp\DATN2\PoPoy.Client\Pages\AccountView\ResetPassword.razor"
    
}
else
{

#line default
#line hidden
#nullable disable
            __builder.OpenElement(8, "section");
            __builder.AddAttribute(9, "class", "login_area section--padding2");
            __builder.OpenElement(10, "div");
            __builder.AddAttribute(11, "class", "container");
            __builder.OpenElement(12, "div");
            __builder.AddAttribute(13, "class", "row");
            __builder.OpenElement(14, "div");
            __builder.AddAttribute(15, "class", "col-lg-6 offset-lg-3");
            __builder.AddAttribute(16, "style", "margin-bottom: 30px;");
            __builder.OpenComponent<Microsoft.AspNetCore.Components.Forms.EditForm>(17);
            __builder.AddAttribute(18, "id", "signinform");
            __builder.AddAttribute(19, "Model", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.Object>(
#nullable restore
#line 27 "D:\Myproject\CSharp\DATN2\PoPoy.Client\Pages\AccountView\ResetPassword.razor"
                                                     resetP

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(20, "OnValidSubmit", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<Microsoft.AspNetCore.Components.EventCallback<Microsoft.AspNetCore.Components.Forms.EditContext>>(Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.Forms.EditContext>(this, 
#nullable restore
#line 27 "D:\Myproject\CSharp\DATN2\PoPoy.Client\Pages\AccountView\ResetPassword.razor"
                                                                            HandleResetPassword

#line default
#line hidden
#nullable disable
            )));
            __builder.AddAttribute(21, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment<Microsoft.AspNetCore.Components.Forms.EditContext>)((context) => (__builder2) => {
                __builder2.OpenElement(22, "div");
                __builder2.AddAttribute(23, "class", "cardify login");
                __builder2.AddMarkupContent(24, "<div class=\"login--header\"><h3>Xin chào</h3>\r\n                                <p>Hãy nhập mật khẩu mới</p></div>\r\n                            ");
                __builder2.OpenComponent<Microsoft.AspNetCore.Components.Forms.DataAnnotationsValidator>(25);
                __builder2.CloseComponent();
                __builder2.AddMarkupContent(26, "\r\n                            ");
                __builder2.OpenComponent<Microsoft.AspNetCore.Components.Forms.ValidationSummary>(27);
                __builder2.CloseComponent();
                __builder2.AddMarkupContent(28, "\r\n                            ");
                __builder2.OpenElement(29, "div");
                __builder2.AddAttribute(30, "class", "login--form");
                __builder2.OpenElement(31, "div");
                __builder2.AddAttribute(32, "class", "form-group");
                __builder2.AddMarkupContent(33, "<label for=\"user_name\">Mật khẩu mới:</label>\r\n                                    ");
                __builder2.OpenComponent<Microsoft.AspNetCore.Components.Forms.InputText>(34);
                __builder2.AddAttribute(35, "id", "username");
                __builder2.AddAttribute(36, "name", "username");
                __builder2.AddAttribute(37, "type", "password");
                __builder2.AddAttribute(38, "class", "text_field");
                __builder2.AddAttribute(39, "placeholder", "Nhập mật khẩu");
                __builder2.AddAttribute(40, "autocomplete", "off");
                __builder2.AddAttribute(41, "required", true);
                __builder2.AddAttribute(42, "autofocus", true);
                __builder2.AddAttribute(43, "Value", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.String>(
#nullable restore
#line 39 "D:\Myproject\CSharp\DATN2\PoPoy.Client\Pages\AccountView\ResetPassword.razor"
                                                                                                  resetP.NewPassword

#line default
#line hidden
#nullable disable
                ));
                __builder2.AddAttribute(44, "ValueChanged", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<Microsoft.AspNetCore.Components.EventCallback<System.String>>(Microsoft.AspNetCore.Components.EventCallback.Factory.Create<System.String>(this, Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.CreateInferredEventCallback(this, __value => resetP.NewPassword = __value, resetP.NewPassword))));
                __builder2.AddAttribute(45, "ValueExpression", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.Linq.Expressions.Expression<System.Func<System.String>>>(() => resetP.NewPassword));
                __builder2.CloseComponent();
                __builder2.AddMarkupContent(46, "\r\n                                    ");
                __Blazor.PoPoy.Client.Pages.AccountView.ResetPassword.TypeInference.CreateValidationMessage_0(__builder2, 47, 48, 
#nullable restore
#line 40 "D:\Myproject\CSharp\DATN2\PoPoy.Client\Pages\AccountView\ResetPassword.razor"
                                                            ()=>resetP.NewPassword

#line default
#line hidden
#nullable disable
                );
                __builder2.CloseElement();
                __builder2.AddMarkupContent(49, "\r\n                                ");
                __builder2.OpenElement(50, "div");
                __builder2.AddAttribute(51, "class", "form-group");
                __builder2.AddMarkupContent(52, "<label for=\"pass\">Xác nhận mật khẩu:</label>\r\n                                    ");
                __builder2.OpenComponent<Microsoft.AspNetCore.Components.Forms.InputText>(53);
                __builder2.AddAttribute(54, "id", "pass");
                __builder2.AddAttribute(55, "name", "pass");
                __builder2.AddAttribute(56, "type", "password");
                __builder2.AddAttribute(57, "class", "text_field");
                __builder2.AddAttribute(58, "placeholder", "Nhập mật khẩu mới");
                __builder2.AddAttribute(59, "required", true);
                __builder2.AddAttribute(60, "Value", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.String>(
#nullable restore
#line 44 "D:\Myproject\CSharp\DATN2\PoPoy.Client\Pages\AccountView\ResetPassword.razor"
                                                            resetP.ConfirmPassword

#line default
#line hidden
#nullable disable
                ));
                __builder2.AddAttribute(61, "ValueChanged", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<Microsoft.AspNetCore.Components.EventCallback<System.String>>(Microsoft.AspNetCore.Components.EventCallback.Factory.Create<System.String>(this, Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.CreateInferredEventCallback(this, __value => resetP.ConfirmPassword = __value, resetP.ConfirmPassword))));
                __builder2.AddAttribute(62, "ValueExpression", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.Linq.Expressions.Expression<System.Func<System.String>>>(() => resetP.ConfirmPassword));
                __builder2.CloseComponent();
                __builder2.AddMarkupContent(63, "\r\n                                    ");
                __Blazor.PoPoy.Client.Pages.AccountView.ResetPassword.TypeInference.CreateValidationMessage_1(__builder2, 64, 65, 
#nullable restore
#line 46 "D:\Myproject\CSharp\DATN2\PoPoy.Client\Pages\AccountView\ResetPassword.razor"
                                                            ()=>resetP.ConfirmPassword

#line default
#line hidden
#nullable disable
                );
                __builder2.CloseElement();
                __builder2.AddMarkupContent(66, "\r\n\r\n                                ");
                __builder2.OpenElement(67, "button");
                __builder2.AddAttribute(68, "class", "btn btn--xs");
                __builder2.AddAttribute(69, "type", "submit");
                __builder2.AddAttribute(70, "disabled", 
#nullable restore
#line 49 "D:\Myproject\CSharp\DATN2\PoPoy.Client\Pages\AccountView\ResetPassword.razor"
                                                                                    isLoading

#line default
#line hidden
#nullable disable
                );
#nullable restore
#line 50 "D:\Myproject\CSharp\DATN2\PoPoy.Client\Pages\AccountView\ResetPassword.razor"
                                     if (isLoading)
                                    {

#line default
#line hidden
#nullable disable
                __builder2.AddMarkupContent(71, "<span class=\"spinner-border spinner-border-sm\" role=\"status\" aria-hidden=\"true\"></span>");
#nullable restore
#line 53 "D:\Myproject\CSharp\DATN2\PoPoy.Client\Pages\AccountView\ResetPassword.razor"
                                    }

#line default
#line hidden
#nullable disable
                __builder2.AddMarkupContent(72, "                                    Lưu\r\n                                ");
                __builder2.CloseElement();
                __builder2.AddMarkupContent(73, "\r\n                                <br>");
                __builder2.CloseElement();
                __builder2.CloseElement();
            }
            ));
            __builder.CloseComponent();
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.CloseElement();
#nullable restore
#line 67 "D:\Myproject\CSharp\DATN2\PoPoy.Client\Pages\AccountView\ResetPassword.razor"
}

#line default
#line hidden
#nullable disable
        }
        #pragma warning restore 1998
#nullable restore
#line 69 "D:\Myproject\CSharp\DATN2\PoPoy.Client\Pages\AccountView\ResetPassword.razor"
       


    bool IsClickLink = false;
    bool isLoading = false;
    ResetPasswordRequest resetP { get; set; } = new ResetPasswordRequest();
    protected override void OnInitialized()
    {

        GetHttpParameters();

    }
    public void GetHttpParameters()
    {
        var uriBuilder = new UriBuilder(navigationManager.Uri);
        var httpQuery = System.Web.HttpUtility.ParseQueryString(uriBuilder.Query);

        resetP.Email = httpQuery["email"] ?? "";
        resetP.Token = httpQuery["token"] ?? "";

        if (!string.IsNullOrEmpty(resetP.Email) && !string.IsNullOrEmpty(resetP.Token))
        {
            IsClickLink = true;

        }
    }
    private async Task HandleResetPassword()
    {

        isLoading = true;
        var result = await AuthService.ResetPassword(resetP);

        if (result.Success)
        {

            _toastSvc.ShowSuccess("Reset mật khẩu thành công");
            await Task.Delay(3000);
        }
        else
        {

            _toastSvc.ShowError(result.Message);

        }
        isLoading = false;
    }

#line default
#line hidden
#nullable disable
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private IAuthService AuthService { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private IToastService _toastSvc { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private NavigationManager navigationManager { get; set; }
    }
}
namespace __Blazor.PoPoy.Client.Pages.AccountView.ResetPassword
{
    #line hidden
    internal static class TypeInference
    {
        public static void CreateValidationMessage_0<TValue>(global::Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder, int seq, int __seq0, global::System.Linq.Expressions.Expression<global::System.Func<TValue>> __arg0)
        {
        __builder.OpenComponent<global::Microsoft.AspNetCore.Components.Forms.ValidationMessage<TValue>>(seq);
        __builder.AddAttribute(__seq0, "For", __arg0);
        __builder.CloseComponent();
        }
        public static void CreateValidationMessage_1<TValue>(global::Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder, int seq, int __seq0, global::System.Linq.Expressions.Expression<global::System.Func<TValue>> __arg0)
        {
        __builder.OpenComponent<global::Microsoft.AspNetCore.Components.Forms.ValidationMessage<TValue>>(seq);
        __builder.AddAttribute(__seq0, "For", __arg0);
        __builder.CloseComponent();
        }
    }
}
#pragma warning restore 1591
