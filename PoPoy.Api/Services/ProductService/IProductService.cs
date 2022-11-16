using Microsoft.AspNetCore.Http;
using PoPoy.Shared.Common;
using PoPoy.Shared.Dto;
using PoPoy.Shared.Paging;
using PoPoy.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PoPoy.Api.Services.ProductService
{
    public interface IProductServices
    {
        Task<List<ProductVM>> GetAllProducts();
        Task<ProductVM> GetById(int productId);
        Task<PagedList<Product>> GetAll(ProductParameters productParameters);
        Task<ServiceResponse<Product>> Get(int id);
        Task<bool> CreateProduct(ProductCreateRequest request);
        Task<bool> UpdateProduct(ProductUpdateRequest request);
        Task<int> DeleteProduct(int productId);
        Task<List<ProductVM>> SearchProduct(string searchText);
        Task<ServiceResponse<List<UploadResult>>> UploadProductImage(List<IFormFile> files, int productId);
        Task<bool> DeleteProductImage(int imageId);
        Task<ServiceResponse<bool>> CategoryAssign(int productId, CategoryAssignRequest request);
        ValueTask<List<ProductQuantity>> FilterAllByIdsAsync(int[] ids, int[] sizes, int[] color);
        Task<PagedList<Product>> GetProductsByCategory(ProductParameters productParameters, string categoryUrl);
        Task<List<ProductSize>> GetSizeProduct(int productId);
        Task<List<ProductSize>> GetAllSizesProduct();
        Task<List<ProductColor>> GetAllColorProduct();
        Task<ServiceResponse<bool>> SizeAssign(int productId, SizeAssignRequest request);
        Task<PagedList<Product>> SearchProducts(ProductParameters productParameters);
        Task<ServiceResponse<List<string>>> GetProductSearchSuggestions(string searchText);
        Task<bool> SeedProduct();
        Task<string> GetProductQuantityAndPrice(int sizeId, int prodId, int colorId);
        Task<string> GetProductVariants(int productId);
        Task<int> DeleteProductVariant(int variantId);
    }
}
