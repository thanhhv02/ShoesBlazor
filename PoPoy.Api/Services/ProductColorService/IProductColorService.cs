using PoPoy.Shared.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PoPoy.Api.Services.ProductColorService
{
    public interface IProductColorService
    {
        Task<List<ProductColorDto>> GetAll();
        Task<ProductColorDto> GetProductColorById(int id);
        Task<bool> CreateProductColor(ProductColorDto productColorDto);
        Task<bool> DeleteProductColorById(int id);
        Task<bool> UpdateProductColor(ProductColorDto productColorDto);
    }
}
