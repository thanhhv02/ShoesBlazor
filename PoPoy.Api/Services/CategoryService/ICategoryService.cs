using PoPoy.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PoPoy.Api.Services.CategoryService
{
    public interface ICategoryService
    {
        Task<List<CateVM>> GetAllCategories();

        Task<CateVM> GetCateById(int id);
    }
}
