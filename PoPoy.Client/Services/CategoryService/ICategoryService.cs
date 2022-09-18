using PoPoy.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PoPoy.Client.Services.CategoryService
{
    public interface ICategoryService
    {
        event Action OnChange;
        List<CateVM> Categories { get; set; }
        Task GetCategories();
    }
}
