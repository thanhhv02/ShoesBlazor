using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PoPoy.Api.Services.ProductService;
using PoPoy.Shared.Dto;
using PoPoy.Shared.Paging;
using PoPoy.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

        [HttpPut("{productId}")]
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
        public async Task<IActionResult> DeleteProduct(int productId)
        {
            var affectedResult = await _productServices.DeleteProduct(productId);
            if (affectedResult == 0)
                return BadRequest();
            return Ok();
        }
        [HttpGet("filter/ids")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async ValueTask<ActionResult<List<Product>>> FilterAllByIds([FromQuery] int[] ids)
        => Ok(await _productServices.FilterAllByIdsAsync(ids));

        [HttpPost("upload-image")]
        public async Task<ActionResult<List<UploadResult>>> UploadFile(List<IFormFile> files, int id)
        {
            var uploadResults = await _productServices.UploadProductImage(files, id);

            return Ok(uploadResults.Data);
        }
        [HttpDelete("delete-image/{imageId}")]
        public async Task<IActionResult> DeleteFile(int imageId)
        {
            var uploadResults = await _productServices.DeleteProductImage(imageId);
            return Ok(uploadResults);
        }
    }
}
