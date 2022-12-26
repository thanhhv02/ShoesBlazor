using PoPoy.Shared.Dto;
using PoPoy.Shared.ViewModels;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace PoPoy.Admin.Services.ProductService
{
    public interface IProductService
    {
        Task<List<ProductVM>> GetAllProducts();
        Task<List<ProductQuantity>> GetAllProductsVariant(int productId);
        Task<List<ProductVM>> SearchProduct(string searchText);
        Task<ProductVM> GetProductById(int productId);
        Task<bool> CreateProduct(ProductCreateRequest request);
        Task UpdateProduct(ProductVM product);
        Task DeleteProduct(int productId);
        Task DeleteProductVariant(int variantId);
        Task DeleteFile(int productId);
        Task AssignCate(CategoryAssignRequest request);
        Task<CategoryAssignRequest> GetCateAssignRequest(int productId);
        Task<HttpResponseMessage> AssignSize(SizeAssignRequest request);
        Task<SizeAssignRequest> GetSizeAssignRequest(int productId);
    }
}
