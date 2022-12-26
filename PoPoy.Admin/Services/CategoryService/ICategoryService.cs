using PoPoy.Shared.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PoPoy.Admin.Services.CategoryService
{
    public interface ICategoryService
    {
        Task<List<CateVM>> GetAllCategories();
        Task<bool> CreateCategory(CategoryCreateRequest request);
        Task<List<CateVM>> SearchCategory(string searchText);
        Task<CateVM> GetCategoryById(int categoryId);
        Task UpdateCategory(CateVM category);
        Task DeleteCategory(int productId);
    }
}
