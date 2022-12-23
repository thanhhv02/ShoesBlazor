using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace PoPoy.Admin.Shared
{
    public partial class MainLayout
    {
        bool IsOpen = true;
        private async Task OpenNav()
        {
            IsOpen = !IsOpen;
            await jSRuntime.InvokeVoidAsync("OpenNav", IsOpen);
        }
    }
}
