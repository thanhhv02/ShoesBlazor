using PoPoy.Shared.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PoPoy.Admin.Services.ProductColorService
{
    public interface IProductColorService
    {
        Task<List<ProductColorDto>> GetAllProductColor();
        Task<ProductColorDto> GetProductColorById(int id);
        Task<bool> CreateProductColor(ProductColorDto model);
        Task<bool> UpdateProductColor(ProductColorDto model);
        Task DeleteProductColor(int id);
    }
}
