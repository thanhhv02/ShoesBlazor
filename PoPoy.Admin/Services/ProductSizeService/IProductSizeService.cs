using PoPoy.Shared.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PoPoy.Admin.Services.ProductSizeService
{
    public interface IProductSizeService
    {
        Task<List<ProductSizeDto>> GetAllProductSize();
        Task<ProductSizeDto> GetProductSizeById(int id);
        Task<bool> CreateProductSize(ProductSizeDto model);
        Task<bool> UpdateProductSize(ProductSizeDto model);
        Task DeleteProductSize(int id);
    }
}
