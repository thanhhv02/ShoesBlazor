using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PoPoy.Api.Helpers;
using PoPoy.Api.Services.ProductService;
using PoPoy.Shared.Dto;
using PoPoy.Shared.Paging;
using PoPoy.Shared.ViewModels;
using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;

namespace PoPoy.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ProductController : ControllerBase
    {
        private readonly IProductServices _productServices;

        public ProductController(IProductServices productServices)
        {
            _productServices = productServices;
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetAll([FromQuery] ProductParameters productParameters)
        {
            var products = await _productServices.GetAll(productParameters);
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(products.MetaData));
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<Product>>> Get(int id)
        {
            return await _productServices.Get(id);
        }

        [HttpGet("getAllProducts")]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _productServices.GetAllProducts();
            return Ok(products);
        }

        [HttpGet("getProductById/{id}" )]
        public async Task<IActionResult> GetProductById(int id)
        {
            var products = await _productServices.GetById(id);
            return Ok(products);
        }

        [HttpPost]
        [AuthorizeToken(AuthorizeToken.ADMIN_STAFF)]

        public async Task<IActionResult> CreateProduct(ProductCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result  = await _productServices.CreateProduct(request);
            return Ok(result);

        }

        [HttpGet("searchProduct/{searchText}")]
        public async Task<IActionResult> SearchProduct(string searchText)
        {
            var result = await _productServices.SearchProduct(searchText);
            if (result.Count == 0)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPut("categories/{id}")]
        [AuthorizeToken(AuthorizeToken.ADMIN_STAFF)]

        public async Task<IActionResult> CategoryAssign(int id, CategoryAssignRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _productServices.CategoryAssign(id, request);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPut("{productId}")]
        [AuthorizeToken(AuthorizeToken.ADMIN_STAFF)]

        public async Task<IActionResult> UpdateProduct(ProductUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _productServices.UpdateProduct(request);
            return Ok(result);
        }

        [HttpDelete("{productId}")]
        [AuthorizeToken(AuthorizeToken.ADMIN_STAFF)]

        public async Task<IActionResult> DeleteProduct(int productId)
        {
            var affectedResult = await _productServices.DeleteProduct(productId);
            if (affectedResult == 0)
                return BadRequest();
            return Ok(affectedResult);
        }
        [HttpGet("filter")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async ValueTask<ActionResult<List<Product>>> FilterAllByIds([FromQuery] int[] ids, [FromQuery] int[] sizes, [FromQuery] int[] color)
        => Ok(await _productServices.FilterAllByIdsAsync(ids, sizes, color));

        [HttpPost("upload-image")]
        public async Task<ActionResult<List<UploadResult>>> UploadFile(List<IFormFile> files, int id)
        {
            var uploadResults = await _productServices.UploadProductImage(files, id);

            return Ok(uploadResults.Data);
        }
        [HttpDelete("delete-image/{productId}")]
        public async Task<IActionResult> DeleteFile(int productId)
        {
            var uploadResults = await _productServices.DeleteProductImage(productId);
            return Ok(uploadResults);
        }
        [HttpGet("category/{categoryUrl}")]
        public async Task<ActionResult<List<Product>>> GetProductsByCategory([FromQuery] ProductParameters productParameters, string categoryUrl)
        {
            var result = await _productServices.GetProductsByCategory(productParameters, categoryUrl);
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(result.MetaData));
            return Ok(result);
        }
        [HttpGet("get-size-product/{id}")]
        public async Task<ActionResult<List<ProductSize>>> GetSizeProduct(int id)
        {
            var result = await _productServices.GetSizeProduct(id);
            return Ok(result);
        }

        [HttpGet("getSizes")]
        public async Task<IActionResult> GetAllSizes()
        {
            var products = await _productServices.GetAllSizesProduct();
            return Ok(products);
        }
        [HttpGet("get-all-color")]
        public async Task<IActionResult> GetAllColor()
        {
            var products = await _productServices.GetAllColorProduct();
            return Ok(products);
        }

        [HttpPut("sizes/{id}")]
        [AuthorizeToken(AuthorizeToken.ADMIN_STAFF)]

        public async Task<IActionResult> SizeAssign(int id, SizeAssignRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _productServices.SizeAssign(id, request);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpGet("search")]
        public async Task<ActionResult<List<Product>>> SearchProducts([FromQuery] ProductParameters productParameters)
        {
            var result = await _productServices.SearchProducts(productParameters);
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(result.MetaData));
            return Ok(result);
        }

        [HttpGet("searchsuggestions/{searchText}")]
        public async Task<ActionResult<ServiceResponse<List<Product>>>> GetProductSearchSuggestions(string searchText)
        {
            var result = await _productServices.GetProductSearchSuggestions(searchText);
            return Ok(result);
        }
        [HttpGet("seed-size-product")]
        public async Task<ActionResult<bool>> SeedProduct()
        {
            var result = await _productServices.SeedProduct();
            if (result)
                return Ok("Da seed");
            else
                return BadRequest("Seed loi");
        }
        [HttpGet("get-product-quantity-price")]
        public async Task<ActionResult<int>> GetQuantityOfProduct(int sizeId, int Prodid, int colorId)
        {
            return Ok(await _productServices.GetProductQuantityAndPrice(sizeId, Prodid,colorId));
        }
        [HttpGet("get-product-variant/{id}")]
        public async Task<ActionResult<string>> GetProductVariants(int id)
        {
            var result = await _productServices.GetProductVariants(id);
            return Ok(result);
        }
    }
}
