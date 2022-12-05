using PoPoy.Shared.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PoPoy.Api.Services.ProductSizeService
{
    public interface IProductSizeService
    {
        Task<List<ProductSizeDto>> GetAll();
        Task<ProductSizeDto> GetProductSizeById(int id);
        Task<bool> CreateProductSize(ProductSizeDto productSizeDto);
        Task<bool> DeleteProductSizeById(int id);
        Task<bool> UpdateProductSize(ProductSizeDto productSizeDto);
    }
}
