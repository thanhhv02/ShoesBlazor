#pragma checksum "D:\Myproject\CSharp\DATN2\PoPoy.Client\Pages\Ultilities\PageInfos.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "db7356335298fa7bb527915a6138f9e05c34dffa"
// <auto-generated/>
#pragma warning disable 1591
namespace PoPoy.Client.Pages.Ultilities
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
    public partial class PageInfos : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
        }
        #pragma warning restore 1998
#nullable restore
#line 3 "D:\Myproject\CSharp\DATN2\PoPoy.Client\Pages\Ultilities\PageInfos.razor"
       
    [Parameter]
    public string Title { get; set; }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await JsRuntime.InvokeVoidAsync("blazorSetTitle", Title + " | Popoy");
    }

#line default
#line hidden
#nullable disable
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private IJSRuntime JsRuntime { get; set; }
    }
}
#pragma warning restore 1591
